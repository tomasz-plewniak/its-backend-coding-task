using System.Diagnostics;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Controllers;

public class ClaimsControllerTests
{
    [Fact]
    public async Task Get_Claims()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ =>
                {});

        var client = application.CreateClient();
        
        var timer = new Stopwatch();
        timer.Start();
        
        var response = await client.GetAsync("/Claims");

        timer.Stop();
        
        response.EnsureSuccessStatusCode();

        //TODO: Apart from ensuring 200 OK being returned, what else can be asserted?
        // 1. Response Content Type: You can assert that the content type of the response is as expected.
        // For example, if you expect JSON data, you can check that the "Content-Type" header indicates "application/json".
        response.Content.Headers.ContentType!.MediaType.Should().Be("application/json");
        
        // 2. Response Body Content: If you expect specific data in the response body, you can assert against it.
        // For example, if you expect certain properties or values in a JSON response:    
        // 3. Response Headers: You can check specific headers present in the response and their values. For instance, checking the "Content-Length" header:
        // 4. Response Time: You can measure the response time and ensure it's within an acceptable range. This helps in performance testing.
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
        
        // 5. Deserialization: If the response is expected to contain structured data like JSON, you can deserialize it
        // into an object and assert against specific properties or values.
        // 6. Error Handling: If the endpoint is expected to return specific error messages under certain conditions,
        // you can assert that the error message is present and correct.
        
    }
}