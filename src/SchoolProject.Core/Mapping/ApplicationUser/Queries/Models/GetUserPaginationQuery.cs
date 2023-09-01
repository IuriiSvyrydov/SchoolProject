namespace SchoolProject.Core.Mapping.ApplicationUser.Queries.Models;

public class GetUserPaginationQuery : IRequest<PaginationResult<GetUserPaginationResponse>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}