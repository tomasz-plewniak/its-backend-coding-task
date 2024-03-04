using MediatR;

namespace ApplicationCore.Functions.Claim.Queries;

public record GetAllClaimsQuery : IRequest<IEnumerable<Entities.Claim>>
{
}
