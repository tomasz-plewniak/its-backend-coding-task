namespace ApplicationCore.Options;

public class CosmosDbOptions
{
    public const string Section = "CosmosDb";

    public string Account { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ClaimContainerName { get; set; } = null!;

    public string CoverContainerName { get; set; } = null!;
}