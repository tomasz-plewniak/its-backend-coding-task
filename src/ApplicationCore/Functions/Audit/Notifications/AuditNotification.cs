using ApplicationCore.Functions.Audit.Enums;
using MediatR;

namespace ApplicationCore.Functions.Audit.Notifications;

public record AuditNotification(string Id, EntityType EntityType, string HttpMethod) : INotification;
