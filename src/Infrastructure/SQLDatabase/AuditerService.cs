using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace Infrastructure.SQLDatabase;

public class AuditerService : IAuditerService
{
    private readonly AuditContext _auditContext;

    public AuditerService(AuditContext auditContext)
    {
        _auditContext = auditContext;
    }
    
    public async Task AuditClaim(string id, string httpRequestType)
    {
        var claimAudit = new ClaimAudit()
        {
            Created = DateTime.Now,
            HttpRequestType = httpRequestType,
            ClaimId = id
        };

        await _auditContext.AddAsync(claimAudit);
        await _auditContext.SaveChangesAsync();
    }
        
    public async Task AuditCover(string id, string httpRequestType)
    {
        var coverAudit = new CoverAudit()
        {
            Created = DateTime.Now,
            HttpRequestType = httpRequestType,
            CoverId = id
        };

        await _auditContext.AddAsync(coverAudit);
        await _auditContext.SaveChangesAsync();
    }
}