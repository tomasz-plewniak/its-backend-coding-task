using MediatR;

namespace ApplicationCore.Functions.Cover.Queries;

public record GetCoverByIdQuery(string Id) : IRequest<Entities.Cover>
{
}