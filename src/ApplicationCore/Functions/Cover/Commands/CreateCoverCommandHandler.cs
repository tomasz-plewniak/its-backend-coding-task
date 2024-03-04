using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Commands;

public class CreateCoverCommandHandler : IRequestHandler<CreateCoverCommand, Entities.Cover>
{
    private readonly ILogger<CreateCoverCommandHandler> _logger;
    private readonly ICoverRepository _coverRepository;

    public CreateCoverCommandHandler(ILogger<CreateCoverCommandHandler> logger, ICoverRepository coverRepository)
    {
        _logger = logger;
        _coverRepository = coverRepository;
    }
    
    public async Task<Entities.Cover> Handle(CreateCoverCommand request, CancellationToken cancellationToken)
    {
        request.Cover.Id = Guid.NewGuid().ToString();
        
        // TODO: Calculate real premium;
        request.Cover.Premium = 100;
        
        await _coverRepository.AddItemAsync(request.Cover);
        
        // TODO: Add audit.
        // _auditer.AuditCover(cover.Id, "POST");

        return request.Cover;
    }
}
