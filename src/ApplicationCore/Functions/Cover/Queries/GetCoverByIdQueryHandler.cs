using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Queries;

public class GetCoverByIdQueryHandler : IRequestHandler<GetCoverByIdQuery, Entities.Cover>
{
    private readonly ILogger<GetCoverByIdQueryHandler> _logger;
    private readonly ICoverRepository _coverRepository;

    public GetCoverByIdQueryHandler(ILogger<GetCoverByIdQueryHandler> logger, ICoverRepository coverRepository)
    {
        _logger = logger;
        _coverRepository = coverRepository;
    }
    
    public Task<Entities.Cover> Handle(GetCoverByIdQuery request, CancellationToken cancellationToken)
    {
        return _coverRepository.GetItemAsync(request.Id, cancellationToken);
    }
}
