using ApplicationCore.Functions.Cover.Commands;
using ApplicationCore.Functions.Cover.Queries;
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
public class CoversController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IValidator<Cover> _coverValidator;
    private readonly IMediator _mediator;

    public CoversController(
        IMapper mapper,
        IValidator<Cover> coverValidator,
        IMediator mediator)
    {
        _mapper = mapper;
        _coverValidator = coverValidator;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync(CancellationToken cancellationToken = default)
    {
        var results = await _mediator.Send(new GetAllCoversQuery(), cancellationToken);
        
        return Ok(_mapper.Map<IEnumerable<Cover>>(results));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetCoverByIdQuery(id), cancellationToken);
        if (response == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<Cover>(response));
    }

    [HttpPost]
    public async Task<ActionResult<Cover>> CreateAsync(Cover cover)
    {
        ValidationResult validationResult = await _coverValidator.ValidateAsync(cover);

        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }
        
        var coverEntity = _mapper.Map<Entities.Cover>(cover);
        var result = await _mediator.Send(new CreateCoverCommand(coverEntity));
        
        return Ok(_mapper.Map<Cover>(result));
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
        await _mediator.Send(new DeleteCoverByIdCommand(id));
    }
}