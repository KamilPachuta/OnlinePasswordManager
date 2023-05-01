using FluentValidation;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Data.Validators
{
    public class PasswordUpdateDetailsDTOValidator : AbstractValidator<PasswordUpdateDetailsDTO>
    {
        public PasswordUpdateDetailsDTOValidator()
        {
            RuleFor(x => x.ServiceName).NotEmpty();

            RuleFor(x => x.Login).NotEmpty();

        }
    }
}
