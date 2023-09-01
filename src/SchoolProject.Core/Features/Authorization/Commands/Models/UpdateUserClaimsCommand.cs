using SchoolProject.Data.DTOs;

namespace SchoolProject.Core.Features.Authorization.Commands.Models;

public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
{

}