using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Notifications;

public class ClaimCreatedNotificationHandler : INotificationHandler<ClaimCreatedNotification>
{
    private readonly ILogger<ClaimCreatedNotificationHandler> _logger;
    private readonly IAuditerService _auditerService;

    public ClaimCreatedNotificationHandler(ILogger<ClaimCreatedNotificationHandler> logger, IAuditerService auditerService)
    {
        _logger = logger;
        _auditerService = auditerService;
    }
    
    public async Task Handle(ClaimCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _auditerService.AuditClaim(notification.Id, HttpMethod.Post.ToString());
    }
}