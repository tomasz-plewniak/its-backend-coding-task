using System.Diagnostics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Controllers;

public class PremiumsControllerTests
{
    [Fact(Skip = "No testing environment available.")]
    public async Task Get_CalculatedPremium()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ =>
                {});

        var client = application.CreateClient();
        
        var timer = new Stopwatch();
        timer.Start();
        
        var response = await client.GetAsync("/Premiums?startDate=2024-05-01&endDate=2024-05-02&coverType=Yacht");

        timer.Stop();
        
        response.EnsureSuccessStatusCode();
        
        response.Content.Headers.ContentType!.MediaType.Should().Be("application/json");

        string content = await response.Content.ReadAsStringAsync();
        decimal.Parse(content).Should().Be(1375);
        
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
}