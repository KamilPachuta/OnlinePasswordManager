using FluentValidation;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Data.Validators
{
    public class PasswordCreateDtoValidator : AbstractValidator<PasswordCreateDTO>
    {
        public PasswordCreateDtoValidator()
        {
            RuleFor(x => x.ServiceName).NotEmpty();

            RuleFor(x => x.Login).NotEmpty();

            RuleFor(x => x.EncryptedPassword).NotEmpty();
        }

    }
}
