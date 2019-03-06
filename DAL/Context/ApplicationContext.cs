using System;
using System.Threading;
using System.Threading.Tasks;
using BLL.Entities;
using EffectiveSoft.Data.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ApplicationContext : IdentityDbContext<User, Role, Guid, UserClaims, UserRole, UserLogin, RoleClaim, UserToken>, IContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
