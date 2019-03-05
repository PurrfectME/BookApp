using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Requests;
using API.Responses;
using BLL.Entities;
using BLL.IdentityWrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ISignInManager _signInManager;
        


        public AccountController(IUserManager userManager, ISignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ResponseUserModel> Register(UserRegisterModel requestModel)
        {
            var userToRegister = (User) requestModel;
            userToRegister.Id = Guid.NewGuid();

            await _userManager.CreateUser(userToRegister, requestModel.Password);

            var responseUser = new ResponseUserModel {Email = userToRegister.Email};

            await _userManager.AddToRole(userToRegister, "Admin");

            return responseUser;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> GenerateToken(AuthorizeUserModel authorizeRequest)
        {
            var actualUser = await _userManager.GetUserByEmail(authorizeRequest.Email);

            await _signInManager.CheckPassword(actualUser, authorizeRequest.Password, false);


        }

    }
}