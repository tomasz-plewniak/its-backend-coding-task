namespace ApplicationCore.Interfaces;

public interface IAuditerService
{
    Task AuditClaim(string id, string httpRequestType);
    
    Task AuditCover(string id, string httpRequestType);
}