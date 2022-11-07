using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QNomy.Domain.Entities;

namespace QNomy.Infrastructure.Contracts
{
    public interface IClientProcessingService
    {
	    Task<Client> GetLastClient(CancellationToken cancellationToken = default);
	    Task AddClient(Client client, CancellationToken cancellationToken = default);

        Task<List<Client>> GetActiveClients(CancellationToken cancellationToken = default);
        Task<List<Client>> GetInLineClients(CancellationToken cancellationToken = default);

        Task<int> ProcessNextClient(CancellationToken cancellationToken = default);
    }
}