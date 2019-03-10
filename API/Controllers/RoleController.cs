using System;
using System.Threading.Tasks;
using API.Requests;
using BLL.Entities;
using BLL.IdentityWrappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;


        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpPost]
        [Route("Create")]
        public async Task<RoleModel> Create(RoleModel roleModel)
        {
            roleModel.Id = Guid.NewGuid();

            await _roleManager.AddRole((Role)roleModel);
            
            return roleModel;
        }

        [HttpPost]
        [Route("UserClaim/{roleName}")]
        public async Task AddClaimToUsers(string roleName)
        {
            await _roleManager.AddClaimsToRole(roleName);
        }
    }
}