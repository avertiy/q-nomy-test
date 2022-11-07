using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using QNomy.Application.Models;
using QNomy.Application.Utilities.Extensions;
using QNomy.Infrastructure.Contracts;

namespace QNomy.Application.Clients.Query.GetClients
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, GetClientsResponse>
    {
        private readonly IClientProcessingService _clientProcessingService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetClientsQueryHandler> _logger;
        public GetClientsQueryHandler(
            ILogger<GetClientsQueryHandler> logger, IClientProcessingService clientProcessingService, IMapper mapper)
        {
            _logger = logger;
            _clientProcessingService = clientProcessingService;
            _mapper = mapper;
        }

        public async Task<GetClientsResponse> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(GetClientsQuery), requestJson);

                var active = await _clientProcessingService.GetActiveClients(cancellationToken);
                var inline = await _clientProcessingService.GetInLineClients(cancellationToken);
                
                return new GetClientsResponse()
                {
                    ClientsInProcess = _mapper.Map<IList<ClientDto>>(active),
                    ClientsInLine = _mapper.Map<IList<ClientDto>>(inline)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(GetClientsQuery), requestJson);
                throw;
            }
        }
    }
}
