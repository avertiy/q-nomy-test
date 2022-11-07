using FluentValidation;
using QNomy.Infrastructure.Contracts;

namespace QNomy.Application.Clients.Command.AddClient
{
    public class CreateUserCommandValidator : AbstractValidator<AddClientCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
