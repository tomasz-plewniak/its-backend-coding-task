using MediatR;

namespace ApplicationCore.Functions.Cover.Notifications;

public record CoverDeletedNotification(string Id) : INotification
{
}
