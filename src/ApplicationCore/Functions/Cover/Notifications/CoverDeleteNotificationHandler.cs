using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Notifications;

public class CoverDeleteNotificationHandler : INotificationHandler<CoverDeletedNotification>
{
    private readonly ILogger<CoverDeleteNotificationHandler> _logger;
    private readonly IAuditerService _auditerService;

    public CoverDeleteNotificationHandler(ILogger<CoverDeleteNotificationHandler> logger, IAuditerService auditerService)
    {
        _logger = logger;
        _auditerService = auditerService;
    }
    
    public async Task Handle(CoverDeletedNotification notification, CancellationToken cancellationToken)
    {
        await _auditerService.AuditCover(notification.Id, HttpMethod.Delete.ToString());
    }
}
