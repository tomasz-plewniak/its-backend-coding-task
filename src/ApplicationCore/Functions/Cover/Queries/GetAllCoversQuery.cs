using MediatR;

namespace ApplicationCore.Functions.Cover.Queries;

public record GetAllCoversQuery : IRequest<IEnumerable<Entities.Cover>>
{
}
