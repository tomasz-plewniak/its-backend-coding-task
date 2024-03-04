using ApplicationCore.Functions.Claim;
using ApplicationCore.Functions.Claim.Commands;
using ApplicationCore.Functions.Claim.Queries;
using ApplicationCore.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.SQLDatabase;
using MediatR;
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
        private readonly IValidator<Claim> _claimValidator;
        private readonly IMediator _mediator;
        private readonly Auditer _auditer;

        public ClaimsController(
            ILogger<ClaimsController> logger,
            AuditContext auditContext,
            IClaimRepository claimRepository,
            IMapper mapper,
            IValidator<Claim> claimValidator,
            IMediator mediator)
        {
            _logger = logger;
            _claimRepository = claimRepository;
            _mapper = mapper;
            _claimValidator = claimValidator;
            _mediator = mediator;
            _auditer = new Auditer(auditContext);
        }

        [HttpGet]
        public async Task<IEnumerable<Claim>> GetAsync()
        {
            var result = await _mediator.Send(new GetAllClaimsQuery(), CancellationToken.None);
            
            return _mapper.Map<IEnumerable<Claim>>(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(Claim claim)
        {
            ValidationResult validationResult = await _claimValidator.ValidateAsync(claim);

            if (validationResult.IsValid == false)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            
            var claimEntity = _mapper.Map<Entities.Claim>(claim);
            
            var result = await _mediator.Send(new CreateClaimCommand(claimEntity));
            
            return Ok(_mapper.Map<Claim>(result));
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
            var result = await _mediator.Send(new GetClaimByIdQuery(id), CancellationToken.None);
            
            return _mapper.Map<Claim>(result);
        }
    }
}