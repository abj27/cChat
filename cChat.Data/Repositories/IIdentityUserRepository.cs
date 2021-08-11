using Microsoft.AspNetCore.Identity;

namespace cChat.Data.Repositories
{
    public interface IIdentityUserRepository
    {
        IdentityUser GetSystemUser();
        IdentityUser GetById(string userId);
    }
}