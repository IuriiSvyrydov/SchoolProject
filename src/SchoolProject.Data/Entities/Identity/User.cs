using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Data.Entities.Identity;

public class User : IdentityUser<int>
{
    public string FullName { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    [EncryptColumn]
    public string? Code { get; set; }
    [InverseProperty(nameof(UserRefreshToken.user))]
    public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new HashSet<UserRefreshToken>();
}
