using SchoolProject.Core.Features.Authorization.Queries.Results;

namespace SchoolProject.Core.Mapping.Roles;

public class GetRoleByIdProfile : Profile
{
    public GetRoleByIdProfile()
    {
        CreateMap<Role, GetRoleByIdResult>();

    }
}