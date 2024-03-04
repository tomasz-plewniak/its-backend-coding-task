using MediatR;

namespace ApplicationCore.Functions.Claim.Queries;

public record GetClaimByIdQuery(String Id) : IRequest<Entities.Claim>
{
}