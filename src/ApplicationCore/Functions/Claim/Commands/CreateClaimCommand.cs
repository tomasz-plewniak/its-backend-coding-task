using MediatR;
using Shared.Enums;

namespace ApplicationCore.Functions.Claim.Commands;

public record CreateClaimCommand(Entities.Claim Claim)
    : IRequest<Entities.Claim>
{
}
