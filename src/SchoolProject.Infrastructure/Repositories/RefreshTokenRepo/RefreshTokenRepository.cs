using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Repositories.RefreshTokenRepo;

public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
{
    private readonly DbSet<UserRefreshToken> _userRefreshTokens;

    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
        _userRefreshTokens = context.Set<UserRefreshToken>();
    }
}