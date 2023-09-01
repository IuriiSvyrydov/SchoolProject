namespace SchoolProject.Core.Mapping.ApplicationUser.Queries.Mapping;

public class GetUserByIdMapping : Profile
{
    public GetUserByIdMapping()
    {
        CreateMap<User, GetUserByIdResponse>();
    }

}