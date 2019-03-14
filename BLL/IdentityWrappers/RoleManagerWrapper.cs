using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.ClaimsTypes;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.IdentityWrappers
{
    public class RoleManagerWrapper : IRoleManager
    {
        private readonly RoleManager<Role> _manager;


        public RoleManagerWrapper(RoleManager<Role> manager)
        {
            _manager = manager;
        }

        public Task<IList<Claim>> GetRoleClaims(string roleName)
        {
            var roles = _manager.Roles.Single(x => x.Name == roleName);
            return _manager.GetClaimsAsync(roles);
        }

        public async Task AddClaimsToRole(string roleName)
        {
            var role = await _manager.FindByNameAsync(roleName);

            await _manager.AddClaimAsync(role, new Claim(BaseClaimTypes.Permission, UserClaim.Add));
        }

        public Task<IdentityResult> AddRole(Role roleToAdd)
        {
            return _manager.CreateAsync(roleToAdd); ;
        }

        public Task<IdentityResult> RemoveRole(Role roleToRemove)
        {
            return _manager.DeleteAsync(roleToRemove);
        }

        public List<Role> GetAllRolesSync()
        {
            return _manager.Roles.ToList();
        }
    }
}
