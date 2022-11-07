using System.Collections.Generic;
using QNomy.Application.Models;

namespace QNomy.Application.Clients.Command.AddClient
{
    public class AddClientResponse
    {
	    public IList<ClientDto> ClientsInLine { get; set; }
	    public IList<ClientDto> ClientsInProcess { get; set; }
    }
}
