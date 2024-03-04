using MediatR;

namespace ApplicationCore.Functions.Cover.Queries;

public record GetCoverByIdQuery(String Id) : IRequest<Entities.Cover>
{
}