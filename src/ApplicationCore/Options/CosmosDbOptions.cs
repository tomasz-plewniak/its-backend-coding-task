namespace ApplicationCore.Options;

public class CosmosDbOptions
{
    public const string Section = "CosmosDb";

    public string Account { get; set; }
    
    public string Key { get; set; }
    
    public string DatabaseName { get; set; }
    
    public string ClaimContainerName { get; set; }
    
    public string CoverContainerName { get; set; }
}