namespace SchoolProject.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<string>>
    {
        public string? NameUa { get; set; }

        public string? NameUs { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? DepartmentId { get; set; }

        public AddStudentCommand(string nameUa)
        {

        }
    }
}