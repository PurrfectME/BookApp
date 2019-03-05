using System.Threading.Tasks;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.IdentityWrappers
{
    public class SignInManagerWrapper : ISignInManager
    {
        private readonly SignInManager<User> _manager;


        public SignInManagerWrapper(SignInManager<User> manager)
        {
            _manager = manager;
        }


        public async Task<SignInResult> CheckPassword(User user, string password, bool lockoutOnFailure)
        {
            return await _manager.CheckPasswordSignInAsync(user, password, lockoutOnFailure); ;
        }

        public async Task Logout()
        {
            await _manager.SignOutAsync();
        }
    }
}
