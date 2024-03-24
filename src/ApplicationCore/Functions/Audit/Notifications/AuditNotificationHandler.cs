using ApplicationCore.Functions.Audit.Enums;
using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Audit.Notifications;

public class AuditNotificationHandler : INotificationHandler<AuditNotification>
{
    private readonly IAuditerService _service;

    public AuditNotificationHandler(IAuditerService service)
    {
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
