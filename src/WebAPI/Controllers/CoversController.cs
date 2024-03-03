using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.SQLDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Shared.Enums;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CoversController : ControllerBase
{
    private readonly ILogger<CoversController> _logger;
    private readonly IPremiumService _premiumService;
    private readonly ICoverRepository _coverRepository;
    private readonly Auditer _auditer;
    private readonly Container _container;

    public CoversController(
        CosmosClient cosmosClient,
        AuditContext auditContext,
        ILogger<CoversController> logger,
        IPremiumService premiumService,
        ICoverRepository coverRepository)
    {
        _logger = logger;
        _premiumService = premiumService;
        _coverRepository = coverRepository;
        _auditer = new Auditer(auditContext);
        _container = cosmosClient?.GetContainer("ClaimDb", "Cover")
                     ?? throw new ArgumentNullException(nameof(cosmosClient));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cover>>> GetAsync()
    {
        var results = await _coverRepository.GetAllAsync();
        
        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cover>> GetAsync(string id)
    {
        var response = await _coverRepository.GetItemAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(Cover cover)
    {
        cover.Id = Guid.NewGuid().ToString();
        cover.Premium = _premiumService.ComputePremium(cover.StartDate, cover.EndDate, cover.Type);
        await _coverRepository.AddItemAsync(cover);
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