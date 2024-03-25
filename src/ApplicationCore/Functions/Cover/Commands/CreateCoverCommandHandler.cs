using ApplicationCore.Functions.Audit.Enums;
using ApplicationCore.Functions.Audit.Notifications;
using ApplicationCore.Functions.Premium.Queries;
using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Cover.Commands;

public class CreateCoverCommandHandler : IRequestHandler<CreateCoverCommand, Entities.Cover>
{
    private readonly ICoverRepository _coverRepository;
    private readonly IMediator _mediator;

    public CreateCoverCommandHandler(
        ICoverRepository coverRepository,
        IMediator mediator)
    {
        _coverRepository = coverRepository;
        _mediator = mediator;
    }
    
    public async Task<Entities.Cover> Handle(CreateCoverCommand request, CancellationToken cancellationToken)
    {
        request.Cover.Id = Guid.NewGuid().ToString();
        
        decimal calculatedPremium = await _mediator.Send(new CalculatePremiumQuery(
            request.Cover.StartDate,
            request.Cover.EndDate,
            request.Cover.Type));
        
        request.Cover.Premium = calculatedPremium;
        
        await _coverRepository.AddItemAsync(request.Cover);
        await _mediator.Publish(new AuditNotification(request.Cover.Id, EntityType.Cover, HttpMethod.Post.ToString()));
        
        return request.Cover;
    }
}
