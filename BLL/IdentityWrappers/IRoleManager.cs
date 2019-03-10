using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.IdentityWrappers
{
    public interface IRoleManager
    {
        Task<IdentityResult> AddRole(Role roleToAdd);
        Task<IdentityResult> RemoveRole(Role roleToRemove);
        List<Role> GetAllRolesSync();
        Task AddClaimsToRole(string roleName);
        Task<IList<Claim>> GetRoleClaims(string roleName);



    }
}
