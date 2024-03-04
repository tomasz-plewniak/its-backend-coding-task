using MediatR;

namespace ApplicationCore.Functions.Claim.Notifications;

public record ClaimDeletedNotification(string Id) : INotification
{
}
