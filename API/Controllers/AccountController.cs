using System;
using System.Threading.Tasks;
using API.Requests;
using API.Responses;
using BLL.Entities;
using BLL.IdentityWrappers;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ISignInManager _signInManager;
        private readonly ITokenService _tokenService;


        public AccountController(IUserManager userManager, ISignInManager signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ResponseUserModel> Register(UserRegisterModel requestModel)
        {
            var userToRegister = (User) requestModel;
            userToRegister.Id = Guid.NewGuid();

            await _userManager.CreateUser(userToRegister, requestModel.Password);

            var responseUser = new ResponseUserModel {Email = userToRegister.Email};

            await _userManager.AddToRole(userToRegister, "User");

            return responseUser;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<object> GenerateToken(AuthorizeUserModel authorizeRequest)
        {
            var actualUser = await _userManager.GetUserByEmail(authorizeRequest.Email);

            await _signInManager.CheckPassword(actualUser, authorizeRequest.Password, false);


            var configuredToken = new
            {
                access_token = _tokenService.GetEncodedJwtToken(),
                userEmail = actualUser.Email
            };

            return configuredToken;
        }

        [HttpPost]
        [Route("Logout")]
        public async Task Logout()
        {
            await _signInManager.Logout();
        }

    }
}