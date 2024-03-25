using ApplicationCore.Functions.Audit.Enums;
using ApplicationCore.Functions.Audit.Notifications;
using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Claim.Commands;

public class DeleteClaimByIdCommandHandler : IRequestHandler<DeleteClaimByIdCommand>
{
    private readonly IClaimRepository _claimRepository;
    private readonly IMediator _mediator;


    public DeleteClaimByIdCommandHandler(
        IClaimRepository claimRepository,
        IMediator mediator)
    {
        _claimRepository = claimRepository;
        _mediator = mediator;
    }
    
    public async Task Handle(DeleteClaimByIdCommand request, CancellationToken cancellationToken)
    {
        await _claimRepository.DeleteItemAsync(request.Id);
        await _mediator.Publish(new AuditNotification(request.Id, EntityType.Claim, HttpMethod.Delete.ToString()));
    }
}
