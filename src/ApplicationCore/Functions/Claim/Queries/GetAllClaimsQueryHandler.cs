using ApplicationCore.Interfaces;
using MediatR;

namespace ApplicationCore.Functions.Claim.Queries;

public class GetAllClaimsQueryHandler : IRequestHandler<GetAllClaimsQuery, IEnumerable<Entities.Claim>>
{
    private readonly IClaimRepository _claimRepository;

    public GetAllClaimsQueryHandler(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }
    
    public Task<IEnumerable<Entities.Claim>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
    {
        return _claimRepository.GetAllAsync(cancellationToken);
    }
}