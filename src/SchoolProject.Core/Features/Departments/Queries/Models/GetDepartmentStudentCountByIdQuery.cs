using SchoolProject.Core.Features.Departments.Queries.Results;

namespace SchoolProject.Core.Features.Departments.Queries.Models;

public class GetDepartmentStudentCountByIdQuery : IRequest<Response<GetDepartmentStudentCountByIdResponse>>
{
    public int DID { get; set; }
}