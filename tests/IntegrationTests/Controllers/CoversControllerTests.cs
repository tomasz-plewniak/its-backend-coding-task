using System.Diagnostics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Controllers;

public class CoversControllerTests
{
    [Fact]
    public async Task Get_Covers()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ =>
                {});

        var client = application.CreateClient();
        
        var timer = new Stopwatch();
        timer.Start();
        
        var response = await client.GetAsync("/Covers");

        timer.Stop();
        
        response.EnsureSuccessStatusCode();
        
        response.Content.Headers.ContentType!.MediaType.Should().Be("application/json");
        
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
}
