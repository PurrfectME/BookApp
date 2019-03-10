using API.Requests;
using BLL.IdentityWrappers;
using FluentValidation;

namespace API.Validators
{
    public class OnAuthorizeUserValidator : AbstractValidator<AuthorizeUserModel>
    {
        public OnAuthorizeUserValidator(IUserManager manager, ISignInManager signInManager)
        {
            var userManager = manager;

            RuleFor(x => x.Email).EmailAddress().NotEmpty().MustAsync( async (model, email, context) =>
            {
                var userResult = await userManager.GetUserByEmail(email);
                return userResult != null;
            }).WithMessage($"Invalid email.");

            RuleFor(x => x.Password).NotEmpty().WithMessage($"Password can't be empty")
                .MustAsync( async (model, email, context) =>
                {
                    var user = await userManager.GetUserByEmail(model.Email);
                    var passwordResult = await signInManager.CheckPassword(user, model.Password, false);
                    return passwordResult.Succeeded;
                }).WithMessage($"Password is incorrect");
        }
    }
}
