using ApplicationCore.Functions.Audit.Enums;
using ApplicationCore.Functions.Audit.Notifications;
using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Claim.Commands;

public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, Entities.Claim>
{
    private readonly IClaimRepository _claimRepository;
    private readonly IMediator _mediator;

    public CreateClaimCommandHandler(IClaimRepository claimRepository,
        IMediator mediator)
    {
        _claimRepository = claimRepository;
        _mediator = mediator;
    }

    public async Task<Entities.Claim> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        request.Claim.Id = Guid.NewGuid().ToString();
        
        await _claimRepository.AddItemAsync(request.Claim);
        await _mediator.Publish(new AuditNotification(request.Claim.Id, EntityType.Claim, HttpMethod.Post.ToString()));
        
        return request.Claim;
    }
}