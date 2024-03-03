using Shared.Enums;

namespace WebAPI.DTOs;

public class Claim : DTO
{
    public string CoverId { get; set; }

    public DateTime Created { get; set; }

    public string Name { get; set; }

    public ClaimType Type { get; set; }
    
    public decimal DamageCost { get; set; }
}
