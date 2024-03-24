using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Claim.Queries;

public class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, Entities.Claim>
{
    private readonly IClaimRepository _claimRepository;

    public GetClaimByIdQueryHandler(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }
    
    public Task<Entities.Claim> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
    {
        return _claimRepository.GetItemAsync(request.Id, cancellationToken);
    }
}
