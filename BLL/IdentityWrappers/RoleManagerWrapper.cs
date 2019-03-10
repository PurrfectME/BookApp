using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.ClaimsTypes;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.IdentityWrappers
{
    public class RoleManagerWrapper : IRoleManager
    {
        private readonly RoleManager<Role> _manager;


        public RoleManagerWrapper(RoleManager<Role> manager)
        {
            _manager = manager;
        }

        public async Task<IList<Claim>> GetRoleClaims(string roleName)
        {
            var roles = await _manager.Roles.SingleAsync(x => x.Name == roleName);
            return await _manager.GetClaimsAsync(roles);
        }

        public async Task AddClaimsToRole(string roleName)
        {
            var role = await _manager.FindByNameAsync(roleName);

            await _manager.AddClaimAsync(role, new Claim(BaseClaimTypes.Permission, UserClaim.Add));
        }

        public async Task<IdentityResult> AddRole(Role roleToAdd)
        {
            return await _manager.CreateAsync(roleToAdd); ;
        }

        public async Task<IdentityResult> RemoveRole(Role roleToRemove)
        {
            return await _manager.DeleteAsync(roleToRemove);
        }

        public List<Role> GetAllRolesSync()
        {
            return _manager.Roles.ToList();
        }
    }
}
