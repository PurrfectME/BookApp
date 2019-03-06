using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using API.Requests;
using API.Responses;
using BLL.Entities;
using BLL.IdentityWrappers;
using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API.Tests
{
    public class AccountTest
    {
        [Fact]
        public async Task Register_new_user()
        {
            var userModel = new UserRegisterModel { Email = "aaaa@gmail.com", Password = "Qqqqqqq1_" };

            //arrange
            var mockedUserManager = new Mock<IUserManager>();
            mockedUserManager.Setup(x => x.AddToRole((User)userModel, "User"))
                .Returns(Task.FromResult(new IdentityResult()));

            var mockedSignInManager = new Mock<ISignInManager>();
            var mockedTokenService = new Mock<ITokenService>();

            var controller = new AccountController(
                mockedUserManager.Object,
                mockedSignInManager.Object,
                mockedTokenService.Object
            );

            //act
            var result = await controller.Register(userModel);

            //assert
            Assert.IsType<ResponseUserModel>(result);
            Assert.NotNull(result);
            mockedUserManager.Verify(user => user.AddToRole(It.IsAny<User>(), It.Is<string>(role => role == "User")), Times.Once);


        }

        [Fact]
        public async Task Generate_jwt_token()
        {
            var userModel = new AuthorizeUserModel() { Email = "newuser@gmail.com", Password = "Qqqqqqq1_" };
            var user = new User {
                AccessFailedCount = 0,
                ConcurrencyStamp = "a1902bd1-a41b-444a-8831-54ee40a91e87",
                Email = userModel.Email,
                EmailConfirmed = false,
                Id = new Guid("AB188833-B28A-454D-8546-4E9DECC46E06"),
                LockoutEnabled = true,
                LockoutEnd = null,
                NormalizedEmail = "NEWUSER@GMAIL.COM",
                NormalizedUserName = "NEWUSER@GMAIL.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEKMROa5mCHYXeoXMSGm8k464VOgusHZB5u1o4WJjhFD03Gls8IGvxEpR2/ApIQTNFA==",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = "WT7MRNUZCMBERNIDY3U4I52BDRCZIESJ",
                TwoFactorEnabled = false,
                UserName = userModel.Email
            };
            IList<string> roles = new List<string>();

            //arrange
            var mockedUserManager = new Mock<IUserManager>();

            mockedUserManager.Setup(x => x.GetUserByEmail(userModel.Email)).Returns(Task.FromResult(user));
            mockedUserManager.Setup(x => x.GetUserRoles(user)).Returns(Task.FromResult(roles));


            var mockedSignInManager = new Mock<ISignInManager>();
            mockedSignInManager.Setup(x => x.CheckPassword((User)userModel, userModel.Password, false))
                .Returns(Task.FromResult(new SignInResult()));

            var mockedTokenService = new Mock<ITokenService>();
            mockedTokenService.Setup(x => x.GetEncodedJwtToken()).Returns(string.Empty);

            var controller = new AccountController(
                mockedUserManager.Object,
                mockedSignInManager.Object,
                mockedTokenService.Object

            );

            //act
            var result = await controller.GenerateToken(userModel);


            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
            mockedUserManager.Verify(x => x.GetUserByEmail(It.Is<string>(email => email == userModel.Email)), Times.Once);
            mockedUserManager.Verify(x => x.GetUserRoles(It.IsAny<User>()), Times.Once);
            mockedSignInManager.Verify(x => x.CheckPassword(It.IsAny<User>(), It.Is<string>(pass => pass == userModel.Password), It.Is<bool>(bl => bl == false)), Times.Once);
            mockedTokenService.Verify(x => x.GetEncodedJwtToken(), Times.Once);

        }
    }
}
