﻿using ApplicationCore.Functions.Cover.Notifications;
using ApplicationCore.Functions.Premium.Queries;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Commands;

public class CreateCoverCommandHandler : IRequestHandler<CreateCoverCommand, Entities.Cover>
{
    private readonly ILogger<CreateCoverCommandHandler> _logger;
    private readonly ICoverRepository _coverRepository;
    private readonly IMediator _mediator;

    public CreateCoverCommandHandler(
        ILogger<CreateCoverCommandHandler> logger,
        ICoverRepository coverRepository,
        IMediator mediator)
    {
        _logger = logger;
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
        await _mediator.Publish(new CoverCreatedNotification(request.Cover.Id));
        
        return request.Cover;
    }
}
