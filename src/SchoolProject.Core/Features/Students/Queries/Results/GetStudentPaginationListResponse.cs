

namespace SchoolProject.Core.Features.Students.Queries.Results;

public class GetStudentPaginationListResponse
{
    public int StudId { get; set; }
    public string? Name { get; set; }

    public string? Address { get; set; }
    public string? DepartmentName { get; set; }
    //public GetStudentPaginationListResponse(int studId, string? name, string? address, string? departmentName)
    //{
    //    StudId = studId;
    //    Name = name;
    //    Address = address;
    //    DepartmentName = departmentName;
    //}

    //public GetStudentPaginationListResponse()
    //{
    //}
}
