
using SchoolProject.Core.Features.Departments.Queries.Results;

namespace SchoolProject.Core.Mapping.Departments;

public class DepartmentByIdProfile : Profile
{
    public DepartmentByIdProfile()
    {
        CreateMap<Department, GetDepartmentByIdResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localized(src.NameUa, src.NameUs)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
            .ForMember(dest => dest.ManagerName, opt => opt.
            MapFrom(src => src.Instructor.Localized(src.Instructor.NameUa, src.Instructor.NameUs)))
            .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
            //.ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));


        CreateMap<DepartmentSubject, SubjectResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localized(src.Subject.SubjectNameUa, src.Subject.SubjectNameUs)));

        //CreateMap<Student, StudentResponse>().ForMember(dest=>dest.Id,
        //opt=>opt.MapFrom(src=>src.StudID))
        //.ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Localized(src.NameUa,src.NameUs)));

        CreateMap<Instructor, InstructorResponse>().ForMember(dest => dest.Id,
        opt => opt.MapFrom(src => src.InsId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localized(src.NameUa, src.NameUs)));



    }
}