using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QNomy.Api.Data;
using QNomy.Domain.Entities;
using QNomy.Infrastructure.Contracts;

namespace QNomy.Infrastructure.Services;

public class ClientProcessingService : IClientProcessingService
{
	private readonly DataContext _context;

	public ClientProcessingService(DataContext context)
	{
		_context = context;
	}
	public Task<List<Client>> GetActiveClients(CancellationToken cancellationToken = default)
	{
		return _context.Clients.Where(x => x.Status == 1).ToListAsync(cancellationToken);
	}

	public Task<List<Client>> GetInLineClients(CancellationToken cancellationToken = default)
	{
		return _context.Clients.Where(x => x.Status == 0).OrderBy(x => x.NumberInLine).ToListAsync(cancellationToken);
	}

	public Task<Client> GetLastClient(CancellationToken cancellationToken = default)
	{
		return _context.Clients.OrderByDescending(x => x.NumberInLine).FirstOrDefaultAsync(cancellationToken);
	}

	public async Task AddClient(Client client, CancellationToken cancellationToken = default)
	{
		_context.Clients.Add(client);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<int> ProcessNextClient(CancellationToken cancellationToken = default)
	{
		//var parameter = new SqlParameter("name", "value");
		var sp_sql = @"EXECUTE dbo.spProcessNext";
		var res = await _context.Database.ExecuteSqlRawAsync(sp_sql, cancellationToken);
		return res;
	}

}