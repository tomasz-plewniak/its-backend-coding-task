using ApplicationCore.Functions.Claim.Notifications;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Claim.Commands;

public class DeleteClaimByIdCommandHandler : IRequestHandler<DeleteClaimByIdCommand>
{
    private readonly ILogger<CreateClaimCommandHandler> _logger;
    private readonly IClaimRepository _claimRepository;
    private readonly IMediator _mediator;


    public DeleteClaimByIdCommandHandler(
        ILogger<CreateClaimCommandHandler> logger,
        IClaimRepository claimRepository,
        IMediator mediator)
    {
        _logger = logger;
        _claimRepository = claimRepository;
        _mediator = mediator;
    }
    
    public async Task Handle(DeleteClaimByIdCommand request, CancellationToken cancellationToken)
    {
        await _claimRepository.DeleteItemAsync(request.Id);
        await _mediator.Publish(new ClaimDeletedNotification(request.Id));
    }
}
