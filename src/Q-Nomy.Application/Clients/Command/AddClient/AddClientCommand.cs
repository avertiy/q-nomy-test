using MediatR;

namespace QNomy.Application.Clients.Command.AddClient
{
    public class AddClientCommand : IRequest<AddClientResponse>
    {
        public string Name { get; set; }
    }
}
