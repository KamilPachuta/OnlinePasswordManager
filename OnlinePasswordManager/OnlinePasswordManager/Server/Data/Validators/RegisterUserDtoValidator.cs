using FluentValidation;
using OnlinePasswordManager.Server.Data.Context;
using OnlinePasswordManager.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePasswordManager.Shared.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public RegisterUserDtoValidator(OnlinePasswordManagerDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken.");
                    }
                });

            RuleFor(x => x.Username)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var usernameInUse = dbContext.users.Any(u => u.Username == value);
                    if (usernameInUse)
                    {
                        context.AddFailure("Username", "That username is taken.");
                    }
                });

            RuleFor(x => x.Password)
                .MinimumLength(10);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);


        }
    }
}
