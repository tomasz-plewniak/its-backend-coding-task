using ApplicationCore.Functions.Claim.Commands;
using ApplicationCore.Functions.Claim.Queries;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using Entities = ApplicationCore.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClaimsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IValidator<Claim> _claimValidator;
    private readonly IMediator _mediator;

    public ClaimsController(
        IMapper mapper,
        IValidator<Claim> claimValidator,
        IMediator mediator)
    {
        _mapper = mapper;
        _claimValidator = claimValidator;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Claim>>> GetAsync(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllClaimsQuery(), cancellationToken);

        return Ok(_mapper.Map<IEnumerable<Claim>>(result));
    }

    [HttpPost]
    public async Task<ActionResult<Claim>> CreateAsync(Claim claim)
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
        return _mediator.Send(new DeleteClaimByIdCommand(id), CancellationToken.None);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Claim>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetClaimByIdQuery(id), cancellationToken);
        if (response == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<Claim>(response));
    }
}