using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.SQLDatabase;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using Entities = ApplicationCore.Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly ILogger<CoversController> _logger;
    private readonly IPremiumService _premiumService;
    private readonly ICoverRepository _coverRepository;
    private readonly IMapper _mapper;
    private readonly Auditer _auditer;

    public CoversController(
        AuditContext auditContext,
        ILogger<CoversController> logger,
        IPremiumService premiumService,
        ICoverRepository coverRepository,
        IMapper mapper)
    {
        _logger = logger;
        _premiumService = premiumService;
        _coverRepository = coverRepository;
        _mapper = mapper;
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
        cover.Id = Guid.NewGuid().ToString();
        cover.Premium = _premiumService.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
        await _coverRepository.AddItemAsync(_mapper.Map<Entities.Cover>(cover));
        _auditer.AuditCover(cover.Id, "POST");
        return Ok(cover);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(string id)
    {
        _auditer.AuditCover(id, "DELETE");
        return _coverRepository.DeleteItemAsync(id);
    }
}