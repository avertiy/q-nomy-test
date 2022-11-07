using System.Collections.Generic;
using System.Text.Json.Serialization;
using QNomy.Application.Models;

namespace QNomy.Application.Clients.Query.GetClients
{
    public class GetClientsResponse
    {
        public IList<ClientDto> ClientsInLine { get; set; }
        public IList<ClientDto> ClientsInProcess { get; set; }
        
        [JsonIgnore]
        public bool HasData => ClientsInLine?.Count > 0 || ClientsInProcess?.Count > 0;
    }
}
