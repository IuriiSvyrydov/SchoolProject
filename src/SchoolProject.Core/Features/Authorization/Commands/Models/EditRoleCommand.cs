using SchoolProject.Data.DTOs;

namespace SchoolProject.Core.Features.Authorization.Commands.Models;

public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
{

}