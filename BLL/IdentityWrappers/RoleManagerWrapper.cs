using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
