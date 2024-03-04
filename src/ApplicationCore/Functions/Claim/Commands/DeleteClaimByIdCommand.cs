using MediatR;
using Shared.Enums;

namespace ApplicationCore.Functions.Claim.Commands;

public record DeleteClaimByIdCommand(string Id) : IRequest
{
}
