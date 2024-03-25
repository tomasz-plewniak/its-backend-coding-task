using MediatR;

namespace ApplicationCore.Functions.Claim.Queries;

public record GetClaimByIdQuery(string Id) : IRequest<Entities.Claim>;