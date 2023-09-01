using SchoolProject.Data.Requests.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models;

public class ManageUserRoleQuery : IRequest<Response<ManageUserRoleResult>>
{
    public int UserId { get; set; }
}