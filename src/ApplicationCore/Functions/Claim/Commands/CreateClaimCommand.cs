using MediatR;

namespace ApplicationCore.Functions.Claim.Commands;

public record CreateClaimCommand(Entities.Claim Claim)
    : IRequest<Entities.Claim>
{
}
