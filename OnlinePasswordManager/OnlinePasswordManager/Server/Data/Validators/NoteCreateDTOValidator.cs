using FluentValidation;
using OnlinePasswordManager.Shared.Models.DTO;

namespace OnlinePasswordManager.Server.Data.Validators
{
    public class NoteCreateDTOValidator : AbstractValidator<NoteCreateDTO>
    {
        public NoteCreateDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
