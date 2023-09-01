namespace SchoolProject.Core.Mapping.Students
{
    public class GetSingleStudentMapping : Profile
    {
        public GetSingleStudentMapping()
        {
            CreateMap<Student, GetSingleStudentResponse>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Localized(src.Department.NameUs, src.Department.NameUa)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localized(src.NameUs, src.NameUa)));
                

        }
    }
}
