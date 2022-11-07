using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using QNomy.Application.Models;
using QNomy.Application.Utilities.Extensions;
using QNomy.Domain.Entities;
using QNomy.Infrastructure.Contracts;

namespace QNomy.Application.Clients.Command.AddClient
{
	public class AddClientCommandHandler : IRequestHandler<AddClientCommand, AddClientResponse>
	{
		private readonly IClientProcessingService _clientProcessingService;
		private readonly ILogger<AddClientCommandHandler> _logger;
		private readonly IMapper _mapper;
		public AddClientCommandHandler(
			ILogger<AddClientCommandHandler> logger, IClientProcessingService clientProcessingService, IMapper mapper)
		{
			_logger = logger;
			_clientProcessingService = clientProcessingService;
			_mapper = mapper;
		}

		public async Task<AddClientResponse> Handle(AddClientCommand request, CancellationToken cancellationToken)
		{
			var requestJson = request.ToJsonString();
			try
			{
				_logger.LogInformation("Start handling {request}: {requestJson}", nameof(AddClientCommand),
					requestJson);

				var last = await _clientProcessingService.GetLastClient(cancellationToken);
				var number = last?.NumberInLine + 1 ?? 1;
				var client = new Client
				{
					FullName = request.Name,
					CheckInTime = DateTime.UtcNow,
					NumberInLine = number,
					Status = 0
				};

				await _clientProcessingService.AddClient(client, cancellationToken);

				var active = await _clientProcessingService.GetActiveClients(cancellationToken);
				var inline = await _clientProcessingService.GetInLineClients(cancellationToken);

				return new AddClientResponse()
				{
					ClientsInProcess = _mapper.Map<IList<ClientDto>>(active),
					ClientsInLine = _mapper.Map<IList<ClientDto>>(inline)
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(AddClientCommand), requestJson);
				throw;
			}
		}
	}
}