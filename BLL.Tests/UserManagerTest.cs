using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Entities;
using DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using Xunit;

namespace BLL.Tests
{
    public class UserManagerTest
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public UserManagerTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("DataBase")
                .Options;
            _context = new ApplicationContext(options);

            //UserManager arrange vars
            IUserStore<User> store = new UserStore<User, Role, ApplicationContext, Guid>(_context);
            IOptions<IdentityOptions> optionsAccessor = new OptionsManager<IdentityOptions>(new OptionsFactory<IdentityOptions>(new List<IConfigureOptions<IdentityOptions>>(), new List<IPostConfigureOptions<IdentityOptions>>()));
            IPasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            IEnumerable<IUserValidator<User>> userValidators = new List<IUserValidator<User>>();
            IEnumerable<IPasswordValidator<User>> passwordValidators = new List<IPasswordValidator<User>>();
            ILookupNormalizer lookupNormalizer = new UpperInvariantLookupNormalizer();
            var describer = new IdentityErrorDescriber();
            ILogger<UserManager<User>> logger = new Logger<UserManager<User>>(new NLogLoggerFactory());

            _userManager = new UserManager<User>(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                lookupNormalizer,
                describer,
                null,
                logger);

        }

        [Fact]
        public async void Create()
        {
            //arrange
            var user = new User { Id = Guid.NewGuid(), Email = "asasa@gmail.com" };
            var userCollection = _context.Users;


            //act
            var result = await _userManager.CreateAsync(user);

            //assert

            Assert.IsType<IdentityResult>(result);
            Assert.Equal(1, _context.Users.Count());

            foreach (var user1 in userCollection)
            {
                Assert.Contains("asasa@gmail.com", user1.Email);
            }
        }

        [Fact]
        public async void Delete()
        {
            //arrange
            var user = new User { Id = Guid.NewGuid(), Email = "d@gmail.com" };


            //act
            var result = await _userManager.DeleteAsync(user);

            //assert
            Assert.IsType<IdentityResult>(result);
            Assert.Equal(0, _context.Users.Count());
        }

        [Fact]
        public async void Password_check_test()
        {
            var user = new User {
                Id = Guid.NewGuid(),
                Email = "jack@gmail.com",
                PasswordHash = "AQAAAAEAACcQAAAAEA9LBu7AjGQ2TNSAXPxboX7fjk8YZynGlCUODSmFpztkqoRwI7N+strDXGguuWHgRg=="
            };

            var result = await _userManager.CheckPasswordAsync(user, "Qqqqqqq1_");

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async void Get_user_roles_test()
        {
            //arrange

            var user = new User {
                Id = Guid.NewGuid(),
                Email = "jack@gmail.com",
                PasswordHash = "AQAAAAEAACcQAAAAEA9LBu7AjGQ2TNSAXPxboX7fjk8YZynGlCUODSmFpztkqoRwI7N+strDXGguuWHgRg=="
            };

            //act
            var result = await _userManager.GetRolesAsync(user);


            //assert
            Assert.IsType<List<string>>(result.ToList());
            Assert.NotEmpty(result);

            foreach (var role in result)
            {
                Assert.Equal("User", role);
            }

        }
    }
}
