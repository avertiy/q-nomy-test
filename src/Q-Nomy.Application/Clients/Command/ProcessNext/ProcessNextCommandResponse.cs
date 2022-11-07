using System.Collections.Generic;
using QNomy.Application.Models;

namespace QNomy.Application.Clients.Command.ProcessNext
{
    public class ProcessNextResponse
    {
	    public IList<ClientDto> ClientsInLine { get; set; }
	    public IList<ClientDto> ClientsInProcess { get; set; }
    }
}
