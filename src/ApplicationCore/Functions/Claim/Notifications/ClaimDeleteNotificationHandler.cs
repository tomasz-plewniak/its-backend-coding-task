using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Notifications;

public class ClaimDeleteNotificationHandler : INotificationHandler<ClaimDeletedNotification>
{
    private readonly ILogger<ClaimDeleteNotificationHandler> _logger;
    private readonly IAuditerService _auditerService;

    public ClaimDeleteNotificationHandler(ILogger<ClaimDeleteNotificationHandler> logger, IAuditerService auditerService)
    {
        _logger = logger;
        _auditerService = auditerService;
    }
    
    public async Task Handle(ClaimDeletedNotification notification, CancellationToken cancellationToken)
    {
        await _auditerService.AuditClaim(notification.Id, HttpMethod.Delete.ToString());
    }
}
