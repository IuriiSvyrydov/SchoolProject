

namespace SchoolProject.Core.Mapping.Students;

public class GetStudentPaginationMapping : Profile
{
    public GetStudentPaginationMapping()
    {
        CreateMap<Student, GetStudentPaginationListResponse>()
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localized(src.Department.NameUs, src.Department.NameUa)))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localized(src.NameUs, src.NameUa)))
               .ForMember(dest => dest.StudId, opt => opt.MapFrom(src => src.StudID))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    }
}
