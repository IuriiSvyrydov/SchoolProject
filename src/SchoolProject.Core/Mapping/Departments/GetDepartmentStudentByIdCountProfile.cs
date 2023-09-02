using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Results;
using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Core.Mapping.Departments;

public class GetDepartmentStudentByIdCountProfile : Profile
{
    public GetDepartmentStudentByIdCountProfile()
    {
        CreateMap<GetDepartmentStudentCountByIdQuery, DepartmentStudentCountProcParameters>();
        CreateMap<DepartmentStudentCountProc, GetDepartmentStudentCountByIdResponse>()
            .ForMember(dest => dest.Name,
                opt
                    => opt.MapFrom(src => src.Localized(src.NameUs, src.NameUa)))
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.StudentCount));



    }
}