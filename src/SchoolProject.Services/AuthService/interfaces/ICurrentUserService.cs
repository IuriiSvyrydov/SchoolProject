namespace SchoolProject.Services.AuthService.interfaces;

public interface ICurrentUserService
{
    Task<User> GetUserAsync();
    int GetUserId();
    Task<List<string>> GetCurrentUserRolesAsync();
}