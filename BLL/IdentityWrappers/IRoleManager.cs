using System.Collections.Generic;
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
    }
}
