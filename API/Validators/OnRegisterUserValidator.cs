using System.Threading.Tasks;
using API.Requests;
using BLL.IdentityWrappers;
using FluentValidation;

namespace API.Validators
{
    public class OnRegisterUserValidator : AbstractValidator<UserRegisterModel>
    {
        private readonly IUserManager _userManager;


        public OnRegisterUserValidator(IUserManager userManager)
        {
            _userManager = userManager;

            

            RuleFor(x => x.Email).EmailAddress().NotEmpty().MustAsync((model, email, context) =>
            {
                var uniqueUser = ValidateOnUniqueUser(email);
                return uniqueUser;
            }).WithMessage($"Current email is already taken.");

            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(20);


        }

        
        private async Task<bool> ValidateOnUniqueUser(string email)
        {
            var userResult = await _userManager.GetUserByEmail(email);
            return userResult == null;
        }
    }
}
