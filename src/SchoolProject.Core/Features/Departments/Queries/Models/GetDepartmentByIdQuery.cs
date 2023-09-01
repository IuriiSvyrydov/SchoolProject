

using SchoolProject.Core.Features.Departments.Queries.Results;

namespace SchoolProject.Core.Features.Departments.Queries.Models;

public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
{
    public int Id { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }


}
