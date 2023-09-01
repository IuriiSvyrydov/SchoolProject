using SchoolProject.Core.Features.Authorization.Queries.Results;

namespace SchoolProject.Core.Mapping.Roles;

public class GetRoleListRoleProfile : Profile
{
    public GetRoleListRoleProfile()
    {
        CreateMap<Role, GetRolesListResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}