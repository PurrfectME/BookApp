using System.Threading.Tasks;
using API.Requests;
using BLL.IdentityWrappers;
using FluentValidation;

namespace API.Validators
{
    public class OnAuthorizeUserValidator : AbstractValidator<AuthorizeUserModel>
    {
        private readonly IUserManager _userManager;
        private readonly ISignInManager _signInManager;

        public OnAuthorizeUserValidator(IUserManager manager, ISignInManager signInManager)
        {
            _userManager = manager;
            _signInManager = signInManager;

            RuleFor(x => x.Email).EmailAddress().NotEmpty().MustAsync((model, email, context) =>
            {
                var result = ValidateOnEmail(email);
                return result;
            }).WithMessage($"Invalid email.");

            RuleFor(x => x.Password).NotEmpty().WithMessage($"Password can't be empty")
                .MustAsync((model, email, context) =>
                {
                    var result = ValidateOnPassword(model);
                    return result;
                }).WithMessage($"Password is incorrect");
        }

        private async Task<bool> ValidateOnEmail(string email)
        {
            var userResult = await _userManager.GetUserByEmail(email);
            return userResult != null;
        }

        private async Task<bool> ValidateOnPassword(AuthorizeUserModel model)
        {
            var user = await _userManager.GetUserByEmail(model.Email);
            var passwordResult = await _signInManager.CheckPassword(user, model.Password, false);

            return passwordResult.Succeeded;
        }
    }
}
