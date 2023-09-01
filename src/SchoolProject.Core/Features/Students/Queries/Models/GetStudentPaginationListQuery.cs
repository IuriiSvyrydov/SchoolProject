using SchoolProject.Data.Enums;

namespace SchoolProject.Core.Features.Students.Queries.Models;

public class GetStudentPaginationListQuery : IRequest<PaginationResult<GetStudentPaginationListResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public SudentOrderEnum OrderBy { get; set; }
    public string? Search { get; set; }
}
