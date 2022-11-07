using FluentValidation;
using QNomy.Infrastructure.Contracts;

namespace QNomy.Application.Clients.Command.AddClient
{
    public class AddClientCommandValidator : AbstractValidator<AddClientCommand>
    {
        public AddClientCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
