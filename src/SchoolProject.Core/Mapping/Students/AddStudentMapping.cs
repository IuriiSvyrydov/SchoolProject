
using SchoolProject.Core.Features.Students.Commands.Models;

namespace SchoolProject.Core.Mapping.Students
{
    public class AddStudentMapping : Profile
    {
        public AddStudentMapping()
        {
            CreateMap<AddStudentCommand, Student>()
                    .ForMember(dest => dest.DID, opt => opt
                    .MapFrom(src => src.DepartmentId));
        }
    }
}