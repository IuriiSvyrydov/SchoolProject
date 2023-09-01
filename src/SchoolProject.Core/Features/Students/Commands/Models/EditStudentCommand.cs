

namespace SchoolProject.Core.Features.Students.Commands.Models;

public class EditStudentCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string NameUa { get; set; }
    public string NameUs { get; set; }
    public string Address { get; set; }
    public string? Phone { get; set; }
    public int DepartmentId { get; set; }
}
