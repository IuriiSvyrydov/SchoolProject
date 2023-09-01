
using SchoolProject.Core.Features.Students.Commands.Models;

namespace SchoolProject.Core.Mapping.Students;

public class EditStudentMapping : Profile
{
    public EditStudentMapping()
    {
        CreateMap<EditStudentCommand, Student>()
            .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.NameUa, opt => opt.MapFrom(src => src.NameUa))
            .ForMember(dest => dest.NameUs, opt => opt.MapFrom(src => src.NameUs))
            .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id));
    }
}
