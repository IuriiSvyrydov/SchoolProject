using SchoolProject.Core.Features.Authorization.Queries.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models;

public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResult>>
{
    public int Id { get; set; }


}