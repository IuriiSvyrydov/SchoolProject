using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Services.Implements;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtSettings _jwtSettings;
    //private readonly ConcurrentDictionary<string, RefreshToken> _refreshTokens;
    private readonly IRefreshTokenRepository _refreshTokensRepository;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly AppDbContext _appDbContext;
    private readonly IEncryptionProvider _encryptionProvider;
    public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokensRepository,
        UserManager<User> userManager, IEmailService emailService, AppDbContext appDbContext)
    {
        _jwtSettings = jwtSettings;
        _refreshTokensRepository = refreshTokensRepository;
        _userManager = userManager;
        _emailService = emailService;
        _appDbContext = appDbContext;
        //_refreshTokens = new ConcurrentDictionary<string, RefreshToken>();
        _encryptionProvider = new GenerateEncryptionProvider("73827F235D5546DD979760F4806A905C");
    }

    #region GenrerateToken
    public async Task<JwtAuthResult> GenerateToken(User user)
    {
        //  var jwtToken = GenerateJWTToken(user);
        var (jwtToken, accessToken) = await GenerateJWTToken(user);
        var refreshToken = GetRefreshToken(user.UserName);
        var userRefreshToken = new UserRefreshToken
        {
            AddedTime = DateTime.Now,
            ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            IsUsed = true,
            JwtId = jwtToken.Id,
            RefreshToken = refreshToken.TokenString,
            Token = accessToken,
            UserId = user.Id
        };
        var createResult = await _refreshTokensRepository.AddAsync(userRefreshToken);
        var response = new JwtAuthResult();
        response.RefreshToken = refreshToken;
        response.AccessToken = accessToken;
        return response;
    }
    #endregion

    public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate)
    {
        // var jwtToken = ReadJwtToken(accessToken);

        var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
        var response = new JwtAuthResult();
        response.AccessToken = newToken;
        var refreshTokenResult = new RefreshToken();
        refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
        refreshTokenResult.TokenString = refreshToken;
        refreshTokenResult.ExpireAt = (DateTime)expiryDate;
        response.RefreshToken = refreshTokenResult;


        return response;
    }

    public async Task<string> ConfirmEmail(int? userId, string? code)
    {
        if (userId is null || code == null)
            return "InvalidCode";
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var confirmedEmail = await _userManager.ConfirmEmailAsync(user, code);
        if (!confirmedEmail.Succeeded)
            return "ErrorConfirmedEmail";
        return "Success";

    }
    public async Task<string> ValidateToken(string accessToken)
    {

        var handler = new JwtSecurityTokenHandler();
        //    var response = handler.ReadJwtToken(accessToken);
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = _jwtSettings.ValidateIssuer,
            ValidIssuers = new[] { _jwtSettings.Issuer },
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key)),
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateLifetime = _jwtSettings.ValidateLifetime
        };
        try
        {
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

            if (validator is null)
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return "Success";
        }
        catch (Exception e)
        {
            return e.Message;
        }

    }

    public JwtSecurityToken ReadJwtToken(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new ArgumentNullException(nameof(accessToken));
        }

        var handler = new JwtSecurityTokenHandler();
        var response = handler.ReadJwtToken(accessToken);

        return response;
    }

    public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
    {
        if (jwtToken is null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        {
            return ("AlgorithmIsWrong", null);
        }

        if (jwtToken.ValidTo > DateTime.UtcNow)
        {
            return ("Token is not Expired", null);
        }

        var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
        var userRefreshToken = await _refreshTokensRepository.GetTableNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Token == accessToken &&
                x.RefreshToken == refreshToken &&
                x.UserId == int.Parse(userId));
        if (userRefreshToken is null)
        {
            return ("RefreshToken is not found", null);
        }
        if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
        {
            userRefreshToken.IsRevoked = true;
            await _refreshTokensRepository.UpdateAsync(userRefreshToken);
            userRefreshToken.IsUsed = false;
            return ("RefreshTokenIsExpired", null);
        }
        var expiryDate = userRefreshToken.ExpiryDate;

        return (userId, expiryDate);
    }


    #region GetRefreshToken

    private RefreshToken GetRefreshToken(string username)
    {
        var refreshToken = new RefreshToken
        {
            ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            UserName = username,
            TokenString = GenerateRefreshToken()
        };
        // _refreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
        return refreshToken;
    }

    #endregion

    #region JwtSecurityToken

    private async Task<(JwtSecurityToken jwtToken, string accessToken)> GenerateJWTToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await GetClaims(user);
        var jwtToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Key)),
                SecurityAlgorithms.HmacSha256));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return (jwtToken, accessToken);

    }

    #endregion
    #region  GenerateRefreshToken()

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        var randomNumberGenerate = RandomNumberGenerator.Create();
        randomNumberGenerate.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    #endregion

    public async Task<List<Claim>> GetClaims(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);
        var claims = new List<Claim>()
        {
         new Claim(ClaimTypes.Name,user.UserName),
         new Claim(ClaimTypes.NameIdentifier,user.UserName),
         new Claim(ClaimTypes.Email,user.Email),
         new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
         new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
         // new Claim(ClaimTypes.Role,"Admin")

        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        claims.AddRange(userClaims);

        return claims;
    }
    public async Task<string> SendResetPasswordCode(string email)
    {
        var trans = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return "UserNotFound";
            //Generate random number

            Random generator = new Random();
            string randomNumber = generator.Next(0, 1000000).ToString("D6");
            user.Code = randomNumber;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) return "ErrorUpdateUser";
            var message = "Code to reset Password:" + user.Code;
            await _emailService.SendEmail(user.Email, message, "Reset Password");
            await trans.CommitAsync();
            return "Success";
        }
        catch (Exception e)
        {
            await trans.RollbackAsync();
            return "Failed";
        }
    }

    public async Task<string> ConfirmResetPassword(string code, string email)
    {
        //Get User
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return "UserNotFound";
        var userCode = user.Code;
        if (userCode == code) return "Success";
        return "Failed";
    }

    public async Task<string> ResetPassword(string email, string password)
    {
        var trans = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            //Get User
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return "UserNotFound";
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, password);
            await trans.CommitAsync();
            return "Success";
        }
        catch (Exception e)
        {
            await trans.RollbackAsync();
            return "Failed";
        }
    }
}
