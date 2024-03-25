using Newtonsoft.Json;
using Shared.Enums;

namespace ApplicationCore.Entities;

public class Claim : Entity
{
    [JsonProperty(PropertyName = "coverId")]
    public string CoverId { get; set; } = null!;

    [JsonProperty(PropertyName = "created")]
    public DateTime Created { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; } = null!;

    [JsonProperty(PropertyName = "claimType")]
    public ClaimType Type { get; set; }

    [JsonProperty(PropertyName = "damageCost")]
    public decimal DamageCost { get; set; }
}
