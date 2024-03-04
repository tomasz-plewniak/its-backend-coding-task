using MediatR;

namespace ApplicationCore.Functions.Claim.Notifications;

public record ClaimCreatedNotification(String Id) : INotification
{
}
