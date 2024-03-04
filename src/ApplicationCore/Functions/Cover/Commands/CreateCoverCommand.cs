using MediatR;

namespace ApplicationCore.Functions.Cover.Commands;

public record CreateCoverCommand(Entities.Cover Cover) : IRequest<Entities.Cover>
{
}
