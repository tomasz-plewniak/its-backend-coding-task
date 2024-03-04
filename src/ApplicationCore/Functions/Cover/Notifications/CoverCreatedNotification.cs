using MediatR;

namespace ApplicationCore.Functions.Cover.Notifications;

public record CoverCreatedNotification(String Id) : INotification
{
}
