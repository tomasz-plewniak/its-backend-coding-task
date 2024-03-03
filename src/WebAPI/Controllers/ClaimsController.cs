using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure;
using Infrastructure.SQLDatabase;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimRepository _claimRepository;
        private readonly Auditer _auditer;

        public ClaimsController(
            ILogger<ClaimsController> logger,
            AuditContext auditContext,
            IClaimRepository claimRepository)
        {
            _logger = logger;
            _claimRepository = claimRepository;
            _auditer = new Auditer(auditContext);
        }

        [HttpGet]
        public async Task<IEnumerable<Claim>> GetAsync()
        {
            return await _claimRepository.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Claim claim)
        {
            claim.Id = Guid.NewGuid().ToString();
            await _claimRepository.AddItemAsync(claim);
            _auditer.AuditClaim(claim.Id, "POST");
            return Ok(claim);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(string id)
        {
            _auditer.AuditClaim(id, "DELETE");
            return _claimRepository.DeleteItemAsync(id);
        }

        [HttpGet("{id}")]
        public Task<Claim> GetAsync(string id)
        {
            return _claimRepository.GetItemAsync(id);
        }
    }
}