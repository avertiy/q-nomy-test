using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using QNomy.Application.Clients.Command.AddClient;
using QNomy.Application.Models;
using QNomy.Application.Utilities.Extensions;
using QNomy.Domain.Entities;
using QNomy.Infrastructure.Contracts;

namespace QNomy.Application.Clients.Command.ProcessNext
{
    public class ProcessNextCommandHandler : IRequestHandler<ProcessNextCommand, ProcessNextResponse>
    {
        private readonly IClientProcessingService _clientProcessingService;
        private readonly ILogger<ProcessNextCommandHandler> _logger;
        private readonly IMapper _mapper;

		public ProcessNextCommandHandler(IClientProcessingService clientProcessingService, ILogger<ProcessNextCommandHandler> logger, IMapper mapper)
        {
	        _clientProcessingService = clientProcessingService;
	        _logger = logger;
	        _mapper = mapper;
        }

        public async Task<ProcessNextResponse> Handle(ProcessNextCommand request, CancellationToken cancellationToken)
        {
	        var requestJson = request.ToJsonString();
	        try
	        {
		        _logger.LogInformation("Start handling {request}: {requestJson}", nameof(ProcessNextCommand), requestJson);

		        await _clientProcessingService.ProcessNextClient(cancellationToken);

				var active = await _clientProcessingService.GetActiveClients(cancellationToken);
				var inline = await _clientProcessingService.GetInLineClients(cancellationToken);

				return new ProcessNextResponse()
		        {
			        ClientsInProcess = _mapper.Map<IList<ClientDto>>(active),
			        ClientsInLine = _mapper.Map<IList<ClientDto>>(inline)
		        };
	        }
	        catch (Exception ex)
	        {
		        _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(ProcessNextCommand), requestJson);
		        throw;
	        }
        }
    }
}
