using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Cover.Queries;

public class GetCoverByIdQueryHandler : IRequestHandler<GetCoverByIdQuery, Entities.Cover>
{
    private readonly ICoverRepository _coverRepository;

    public GetCoverByIdQueryHandler(ICoverRepository coverRepository)
    {
        _coverRepository = coverRepository;
    }
    
    public Task<Entities.Cover> Handle(GetCoverByIdQuery request, CancellationToken cancellationToken)
    {
        return _coverRepository.GetItemAsync(request.Id, cancellationToken);
    }
}
