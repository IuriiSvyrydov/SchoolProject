using SchoolProject.Data.Requests.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models;

public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
{
    public int UserId { get; set; }
}