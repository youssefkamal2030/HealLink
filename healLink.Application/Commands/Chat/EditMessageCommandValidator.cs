using FluentValidation;

namespace healLink.Application.Commands.Chat
{
    public class EditMessageCommandValidator : AbstractValidator<EditMessageCommand>
    {
        public EditMessageCommandValidator()
        {
            RuleFor(x => x.MessageId)
                .NotEmpty()
                .WithMessage("Message ID is required.");

          

            RuleFor(x => x.NewContent)
                .NotEmpty()
                .WithMessage("Message content cannot be empty.")
                .MaximumLength(5000)
                .WithMessage("Message content cannot exceed 5000 characters.");
        }
    }
}
