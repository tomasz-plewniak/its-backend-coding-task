using ApplicationCore.Functions.Cover.Commands;
using ApplicationCore.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.SQLDatabase;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using Entities = ApplicationCore.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly ILogger<CoversController> _logger;
    private readonly ICalculatePremiumService _calculatePremiumService;
    private readonly ICoverRepository _coverRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<Cover> _coverValidator;
    private readonly IMediator _mediator;
    private readonly Auditer _auditer;

    public CoversController(
        AuditContext auditContext,
        ILogger<CoversController> logger,
        ICalculatePremiumService calculatePremiumService,
        ICoverRepository coverRepository,
        IMapper mapper,
        IValidator<Cover> coverValidator,
        IMediator mediator)
    {
        _logger = logger;
        _calculatePremiumService = calculatePremiumService;
        _coverRepository = coverRepository;
        _mapper = mapper;
        _coverValidator = coverValidator;
        _mediator = mediator;
        _auditer = new Auditer(auditContext);
    }

    [HttpGet]
    public async Task<ActionResult<Cover>> GetAsync()
    {
        var results = await _coverRepository.GetAllAsync();
        
        return Ok(_mapper.Map<IEnumerable<Cover>>(results));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
        var response = await _coverRepository.GetItemAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<Cover>(response));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Cover cover)
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