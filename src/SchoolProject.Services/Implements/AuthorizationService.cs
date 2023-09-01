using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Requests.Results;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Services.Implements;

public class AuthorizationService : IAuthorizationService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _context;

    public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDbContext appDbContext)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = appDbContext;
    }

    public async Task<string> AddRoleAsync(string roleName)
    {
        var identityRole = new Role();
        identityRole.Name = roleName;
        var result = await _roleManager.CreateAsync(identityRole);
        if (result.Succeeded)
            return "Success";
        return "Failed";
    }

    public async Task<string> EditRoleAsync(EditRoleRequest request)
    {
        //check role exist
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role is null)
            return "NotFound";
        role.Name = request.Name;
        var result = await _roleManager.UpdateAsync(role);
        if (result.Succeeded) return "Success";
        var errors = string.Join("-", result.Errors);
        return errors;


    }

    public async Task<string> DeleteRoleAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role == null) return "NotFound";
        var users = await _userManager.GetUsersInRoleAsync(role.Name);
        if (users != null && users.Count > 0) return "Used";
        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded) return "Success";
        var errors = string.Join("-", result.Errors);
        return errors;
    }

    public async Task<bool> IsRoleExistById(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return false;
        return true;
    }

    public async Task<List<Role>> GetRoleListAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }
    public async Task<bool> IsRoleExistByName(string roleName)
    {
        var result = await _roleManager.RoleExistsAsync(roleName);
        return result;
    }

    public async Task<Role> GetRoleById(int id)
    {
        return await _roleManager.FindByIdAsync(id.ToString());
    }

    public async Task<ManageUserRoleResult> ManageUserRolesData(User user)
    {
        var response = new ManageUserRoleResult();
        var userRolesList = new List<UserRoles>();
        //Get userRoles
        //  var userRole = await _userManager.GetRolesAsync(user);
        //Get Roles
        var roles = await _roleManager.Roles.ToListAsync();
        response.UserId = user.Id;
        foreach (var role in roles)
        {
            var userrole = new UserRoles();
            userrole.Id = role.Id;
            userrole.Name = role.Name;
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                userrole.IsHasRole = true;
            }
            else
            {
                userrole.IsHasRole = false;
            }
            userRolesList.Add(userrole);
        }
        response.RolesList = userRolesList;
        return response;

    }

    public async Task<string> UpdateUserRoles(UpdateUserRoleRequest request)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            //GetUserAsync
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
            {
                return "UserIsNull";
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!removeResult.Succeeded)
                return "FailedToRemoveOldRole";
            var selectedRole = request.RolesList.Where(x => x.IsHasRole == true)
                .Select(x => x.Name);
            var addRoleResult = await _userManager.AddToRolesAsync(user, selectedRole);
            if (!addRoleResult.Succeeded)
                return "FailedAddedNewRole";
            await _context.Database.CommitTransactionAsync();
            return "Success";

        }
        catch (Exception a)
        {
            await _context.Database.RollbackTransactionAsync();
            return "FailedToAddRoles";
        }

    }
    public async Task<ManageUserClaimsResult> ManageUserClaimData(User user)
    {
        var response = new ManageUserClaimsResult();
        var userClaimsList = new List<UserClaims>();
        response.UserId = user.Id;
        //get user
        var userClaims = await _userManager.GetClaimsAsync(user);
        foreach (var claim in ClaimStore.Claims)
        {
            var userClaim = new UserClaims();
            userClaim.Type = claim.Type;
            if (userClaims.Any(x => x.Type == claim.Type))
            {
                userClaim.Value = true;
            }
            else
            {
                userClaim.Value = false;
            }
            userClaimsList.Add(userClaim);
        }

        response.UserClaims = userClaimsList;
        return response;
    }

    public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
    {
        var transact = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user is null)
            {
                return "UserIsNull";
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
            if (!removeClaimsResult.Succeeded) return "FailedToRemoveClaims";

            var claims = request.UserClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
            var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
            if (!addUserClaimResult.Succeeded)
                return "FailedRoAddNewClaims";

            await _context.Database.CommitTransactionAsync();
            return "Success";
        }
        catch (Exception e)
        {
            await _context.Database.RollbackTransactionAsync();
            return "FailedToUpdateClaims";
        }
    }
}