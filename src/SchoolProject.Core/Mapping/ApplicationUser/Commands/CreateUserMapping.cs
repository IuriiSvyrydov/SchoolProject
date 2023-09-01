
namespace SchoolProject.Core.Mapping.ApplicationUser.Commands;

public class CreateUserMapping : Profile
{
    public CreateUserMapping()
    {
        CreateMap<AddUserCommand, User>()
            //.ForMember(dest=>dest.Address,opt=>opt.MapFrom(src=>src.Address))
            //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            ;
    }
}