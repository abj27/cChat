using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace cChat.Data.Repositories
{
    public class IdentityUserRepository:  IIdentityUserRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public IdentityUserRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IdentityUser GetSystemUser()
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName == "System");
        }

        public IdentityUser GetById(string userId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id== userId);
        }
    }
}
