using SchoolProject.Core.Features.Departments.Queries.Results;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Core.Mapping.Departments;

public class GetDepartmentStudentCountProfile : Profile
{
    public GetDepartmentStudentCountProfile()
    {
        CreateMap<ViewDepartment, GetDepartmentStudentCountResponse>()
            .ForMember(x => x.Name,
                opt => opt.MapFrom(src =>
                    src.Localized(src.NameUa, src.NameUs)))
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));
    }
}