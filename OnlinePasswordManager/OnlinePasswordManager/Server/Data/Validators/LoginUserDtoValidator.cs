using FluentValidation;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Data.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<UserLoginDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Password).MinimumLength(10);
        }
    }
}
