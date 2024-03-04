using ApplicationCore.Functions.Claim.Queries;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Functions.Cover.Queries;

public class GetAllCoversQueryHandler : IRequestHandler<GetAllCoversQuery, IEnumerable<Entities.Cover>>
{
    private readonly ILogger<GetAllClaimsQueryHandler> _logger;
    private readonly ICoverRepository _coverRepository;

    public GetAllCoversQueryHandler(ILogger<GetAllClaimsQueryHandler> logger, ICoverRepository coverRepository)
    {
        _logger = logger;
        _coverRepository = coverRepository;
    }
    
    public Task<IEnumerable<Entities.Cover>> Handle(GetAllCoversQuery request, CancellationToken cancellationToken)
    {
        return _coverRepository.GetAllAsync(cancellationToken);
    }
}