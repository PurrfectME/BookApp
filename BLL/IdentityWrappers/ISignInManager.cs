using System.Threading.Tasks;
using BLL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.IdentityWrappers
{
    public interface ISignInManager
    {
        Task<SignInResult> CheckPassword(User user, string password, bool lockoutOnFailure);
        Task Logout();
    }
}
