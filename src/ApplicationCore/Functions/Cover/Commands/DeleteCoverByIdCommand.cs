using MediatR;

namespace ApplicationCore.Functions.Cover.Commands;

public record DeleteCoverByIdCommand(string Id) : IRequest
{
}
