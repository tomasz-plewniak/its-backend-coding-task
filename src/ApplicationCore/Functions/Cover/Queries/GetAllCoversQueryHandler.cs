using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Cover.Queries;

public class GetAllCoversQueryHandler : IRequestHandler<GetAllCoversQuery, IEnumerable<Entities.Cover>>
{
    private readonly ICoverRepository _coverRepository;

    public GetAllCoversQueryHandler(ICoverRepository coverRepository)
    {
        _coverRepository = coverRepository;
    }
    
    public Task<IEnumerable<Entities.Cover>> Handle(GetAllCoversQuery request, CancellationToken cancellationToken)
    {
        return _coverRepository.GetAllAsync(cancellationToken);
    }
}