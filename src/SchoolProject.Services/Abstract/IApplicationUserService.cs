namespace SchoolProject.Services.Abstract;

public interface IApplicationUserService
{
    Task<string> AddUserAsync(User user, string password);
}