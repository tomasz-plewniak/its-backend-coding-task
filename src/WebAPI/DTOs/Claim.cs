using Shared.Enums;

namespace WebAPI.DTOs;

public class Claim : Dto
{
    public string CoverId { get; set; } = null!;

    public DateTime Created { get; set; }

    public string Name { get; set; } = null!;

    public ClaimType Type { get; set; }
    
    public decimal DamageCost { get; set; }
}
