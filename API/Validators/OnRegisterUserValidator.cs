using API.Requests;
using BLL.IdentityWrappers;
using FluentValidation;

namespace API.Validators
{
    public class OnRegisterUserValidator : AbstractValidator<UserRegisterModel>
    {
        public OnRegisterUserValidator(IUserManager userManager)
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MustAsync( async (model, email, context) =>
            {
                var userResult = await userManager.GetUserByEmail(email);
                return userResult == null;
            }).WithMessage($"Current email is already taken.");

            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(20);


        }
    }
}
