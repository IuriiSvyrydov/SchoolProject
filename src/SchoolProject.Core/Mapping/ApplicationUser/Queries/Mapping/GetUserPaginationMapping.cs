namespace SchoolProject.Core.Mapping.ApplicationUser.Queries.Mapping;

public class GetUserPaginationMapping : Profile
{
    public GetUserPaginationMapping()
    {
        CreateMap<User, GetUserPaginationResponse>();
    }
}