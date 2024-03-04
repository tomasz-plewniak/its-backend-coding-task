using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Notifications;

public class CoverCreatedNotificationHandler : INotificationHandler<CoverCreatedNotification>
{
    private readonly ILogger<CoverCreatedNotificationHandler> _logger;
    private readonly IAuditerService _auditerService;

    public CoverCreatedNotificationHandler(ILogger<CoverCreatedNotificationHandler> logger, IAuditerService auditerService)
    {
        _logger = logger;
        _auditerService = auditerService;
    }
    
    public async Task Handle(CoverCreatedNotification notification, CancellationToken cancellationToken)
    {
        await _auditerService.AuditCover(notification.Id, HttpMethod.Post.ToString());
    }
}