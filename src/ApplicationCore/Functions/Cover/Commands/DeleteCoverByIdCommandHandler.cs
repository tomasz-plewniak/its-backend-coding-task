using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Commands;

public class DeleteCoverByIdCommandHandler : IRequestHandler<DeleteCoverByIdCommand>
{
    private readonly ILogger<CreateCoverCommandHandler> _logger;
    private readonly ICoverRepository _coverRepository;

    public DeleteCoverByIdCommandHandler(ILogger<CreateCoverCommandHandler> logger, ICoverRepository coverRepository)
    {
        _logger = logger;
        _coverRepository = coverRepository;
    }
    
    public async Task Handle(DeleteCoverByIdCommand request, CancellationToken cancellationToken)
    {
        await _coverRepository.DeleteItemAsync(request.Id);
        // TODO: Add audit.
        // _auditer.AuditCover(id, "DELETE");
    }
}
