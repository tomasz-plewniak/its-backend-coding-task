using ApplicationCore.Functions.Audit.Enums;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Audit.Notifications;

public class AuditNotificationHandler : INotificationHandler<AuditNotification>
{
    private readonly ILogger<AuditNotificationHandler> _logger;
    private readonly IAuditerService _service;

    public AuditNotificationHandler(ILogger<AuditNotificationHandler> logger, IAuditerService service)
    {
        _logger = logger;
        _service = service;
    }
    
    public Task Handle(AuditNotification notification, CancellationToken cancellationToken)
    {
        switch (notification.EntityType)
        {
            case EntityType.Claim:
                _service.AuditClaim(notification.Id, notification.HttpMethod);
                break;
            case EntityType.Cover:
                _service.AuditCover(notification.Id, notification.HttpMethod);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(notification.EntityType), "Unknown entity type to audit.");
        }

        return Task.CompletedTask;
    }
}
