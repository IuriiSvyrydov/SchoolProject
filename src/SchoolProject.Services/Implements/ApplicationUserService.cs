using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Services.Implements;

public class ApplicationUserService : IApplicationUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _appDbContext;
    private readonly IUrlHelper _urlHelper;

    public ApplicationUserService(UserManager<User> userManager, IEmailService emailService,
        IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext, IUrlHelper urlHelper)
    {
        _userManager = userManager;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _appDbContext = appDbContext;
        _urlHelper = urlHelper;
    }
    public async Task<string> AddUserAsync(User user, string password)
    {
        var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            //check if email is Exist
            var existUser = await _userManager.FindByEmailAsync(user.Email);
            if (existUser is not null)
                return "EmailIsExist";
            //check if username exist
            var userByName = await _userManager.FindByNameAsync(user.UserName);
            if (userByName is not null)
                return "UserNameIsExist";
            //Mapping

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return string.Join(",", result.Errors.Select(x => x.Description).ToList());
            await _userManager.AddToRoleAsync(user, "User");
            //Send Email

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var requestAccessor = _httpContextAccessor.HttpContext.Request;
            var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host +
                             _urlHelper.Action("ConfirmEmail", "Authentication", new { usrId = user.Id, code = code });
            var message = $"To Confirm Emali click link: <a href='{returnUrl}'></a>";
            /* $"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";*/
            var sendEmailResult = await _emailService.SendEmail(user.Email, message, "Confirm Email");
            if (sendEmailResult == "Failed")
                return "FailedToSend";
            await transaction.CommitAsync();
            return "Success";
        }
        catch (Exception e)
        {

            await transaction.RollbackAsync();
            return "Failed";
        }



    }
}