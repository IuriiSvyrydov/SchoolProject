namespace SchoolProject.Core.Mapping.ApplicationUser.Commands;

public class UpdateUserMapping : Profile
{
    public UpdateUserMapping()
    {
        CreateMap<UpdateUserCommand, User>();
    }
}