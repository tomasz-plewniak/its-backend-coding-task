using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.SQLDatabase;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using Entities = ApplicationCore.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        
        private readonly ILogger<ClaimsController> _logger;
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;
        private readonly Auditer _auditer;

        public ClaimsController(
            ILogger<ClaimsController> logger,
            AuditContext auditContext,
            IClaimRepository claimRepository,
            IMapper mapper)
        {
            _logger = logger;
            _claimRepository = claimRepository;
            _mapper = mapper;
            _auditer = new Auditer(auditContext);
        }

        [HttpGet]
        public async Task<IEnumerable<Claim>> GetAsync()
        {
            var result = await _claimRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Claim>>(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Claim claim)
        {
            claim.Id = Guid.NewGuid().ToString();
            await _claimRepository.AddItemAsync(_mapper.Map<Entities.Claim>(claim));
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
        public async Task<Claim> GetAsync(string id)
        {
            var result = await _claimRepository.GetItemAsync(id);
            return _mapper.Map<Claim>(result);
        }
    }
}