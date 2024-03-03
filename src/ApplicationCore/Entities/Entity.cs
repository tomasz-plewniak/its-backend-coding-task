using Newtonsoft.Json;

namespace ApplicationCore.Entities;

public abstract class Entity
{
    [JsonProperty(PropertyName = "id")] public string Id { get; set; }
}