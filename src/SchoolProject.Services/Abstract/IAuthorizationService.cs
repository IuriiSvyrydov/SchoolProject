using SchoolProject.Data.DTOs;
using SchoolProject.Data.Requests.Results;

namespace SchoolProject.Services.Abstract;

public interface IAuthorizationService
{
    Task<string> AddRoleAsync(string roleName);
    Task<bool> IsRoleExistByName(string roleName);
    Task<string> EditRoleAsync(EditRoleRequest request);
    Task<string> DeleteRoleAsync(int id);
    Task<bool> IsRoleExistById(int roleId);
    Task<List<Role>> GetRoleListAsync();
    Task<Role> GetRoleById(int id);
    Task<ManageUserRoleResult> ManageUserRolesData(User user);
    Task<string> UpdateUserRoles(UpdateUserRoleRequest request);
    Task<ManageUserClaimsResult> ManageUserClaimData(User user);
    Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
}