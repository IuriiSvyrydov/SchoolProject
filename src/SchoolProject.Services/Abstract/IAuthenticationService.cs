namespace SchoolProject.Services.Abstract;

public interface IAuthenticationService
{
    Task<JwtAuthResult> GenerateToken(User user);
    //  Task<JwtAuthResult> GetRefreshToken(string accessToken, string refreshToken);
    Task<string> ValidateToken(string accessToken);
    JwtSecurityToken ReadJwtToken(string accessToken);
    Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
    Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate);
    Task<string> ConfirmEmail(int? userId, string? code);
    Task<string> SendResetPasswordCode(string email);
    Task<string> ConfirmResetPassword(string code, string email);
    Task<string> ResetPassword(string email, string password);
}