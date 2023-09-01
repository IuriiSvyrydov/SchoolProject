using SchoolProject.Data.DTOs;

namespace SchoolProject.Core.Features.Authorization.Commands.Models;

public class UpdateUserRolesCommand : UpdateUserRoleRequest, IRequest<Response<string>>
{
}