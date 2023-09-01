namespace SchoolProject.Core.Mapping.Students
{
    public class GetStudentListMappimg : Profile
    {
        public GetStudentListMappimg()
        {
            CreateMap<Student, GetStudentListResponse>()
               .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Localized(src.Department.NameUs, src.Department.NameUa)))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localized(src.NameUs, src.NameUa)));
        }
    }
}
