using System.Net;

using Microsoft.AspNetCore.Mvc.Testing;

using Shouldly;

using TodoApp.Server;

namespace Todo.App.Tests.Integration.Endpoints;

public class FailingHealthCheckFactory : WebApplicationFactory<ITodoAppMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting(
            "ConnectionStrings:TodoApi",
            "Data Source=localhost,9443;Connect Timeout=1;Trust Server Certificate=True;");
        base.ConfigureWebHost(builder);
    }
}

public class FailingHealthCheckTests(FailingHealthCheckFactory factory)
    : IClassFixture<FailingHealthCheckFactory>
{
    [Fact]
    public async Task HealthCheck_WhenUnhealthy_ShouldReturnUnhealthy()
    {
        // Arrange
        using var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/_health");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.ServiceUnavailable);

        var message = await response.Content.ReadAsStringAsync();
        message.ShouldBe("Unhealthy");
    }
}