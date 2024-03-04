using MediatR;

namespace ApplicationCore.Functions.Claim.Commands;

public record DeleteClaimByIdCommand(string Id) : IRequest
{
}
