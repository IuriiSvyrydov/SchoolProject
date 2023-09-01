using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.InfrastructureBase.Abstracts;

public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
{

}