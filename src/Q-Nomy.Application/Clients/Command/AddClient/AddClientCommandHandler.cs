using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using QNomy.Application.Utilities.Extensions;
using QNomy.Domain.Entities;
using QNomy.Infrastructure.Contracts;

namespace QNomy.Application.Clients.Command.AddClient
{
    public class CreateUserCommandHandler : IRequestHandler<AddClientCommand, AddClientResponse>
    {
        private readonly IClientProcessingService _clientProcessingService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(
            ILogger<CreateUserCommandHandler> logger, IClientProcessingService clientProcessingService)
        {
            _logger = logger;
            _clientProcessingService = clientProcessingService;
        }

        public async Task<AddClientResponse> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            var requestJson = request.ToJsonString();
            try
            {
                _logger.LogInformation("Start handling {request}: {requestJson}", nameof(AddClientCommand), requestJson);

                var last = await _clientProcessingService.GetLastClient(cancellationToken);
                var number = last?.NumberInLine+1 ?? 1;
                var client = new Client
                {
                    FullName = request.Name,
                    CheckInTime = DateTime.UtcNow,
                    NumberInLine = number,
                    Status = 0
                };

                await _clientProcessingService.AddClient(client, cancellationToken);
                return new AddClientResponse() { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handle {request}: {requestJson} failed", nameof(AddClientCommand), requestJson);
                throw;
            }
        }
    }
}
