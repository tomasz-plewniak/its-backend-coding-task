using ApplicationCore.Functions.Cover.Notifications;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Commands;

public class DeleteCoverByIdCommandHandler : IRequestHandler<DeleteCoverByIdCommand>
{
    private readonly ILogger<CreateCoverCommandHandler> _logger;
    private readonly ICoverRepository _coverRepository;
    private readonly IMediator _mediator;

    public DeleteCoverByIdCommandHandler(
        ILogger<CreateCoverCommandHandler> logger,
        ICoverRepository coverRepository,
        IMediator mediator)
    {
        _logger = logger;
        _coverRepository = coverRepository;
        _mediator = mediator;
    }
    
    public async Task Handle(DeleteCoverByIdCommand request, CancellationToken cancellationToken)
    {
        await _coverRepository.DeleteItemAsync(request.Id);
        await _mediator.Publish(new CoverDeletedNotification(request.Id));
    }
}
