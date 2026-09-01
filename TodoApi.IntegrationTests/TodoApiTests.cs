using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Services;

namespace TodoApi.IntegrationTests;

public class TodoApiTests(
    CustomWebApplicationFactory<Program> factory) :
    IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient(new()
    {
        AllowAutoRedirect = false
    });
    private readonly CustomWebApplicationFactory<Program>
        _factory = factory;

    [Fact]
    public async Task Post_CreateTodo_ReturnsCreated()
    {
        // Arrange
        var request = new TodoItemDto();

        //Act
        var response = await _client.PostAsJsonAsync("/todoitems", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.StartsWith("/todoitems/", response.Headers.Location?.OriginalString);
    }

    // Quote ©1975 BBC: The Doctor (Tom Baker); Pyramids of Mars
    // https://www.bbc.co.uk/programmes/p00pys55
    public class TestQuoteService : IQuoteService
    {
        public Task<string> GenerateQuote()
        {
            return Task.FromResult(
                "Something's interfering with time, Mr. Scarman, " +
                "and time is my business.");
        }
    }

    [Fact]
    public async Task Get_QuoteService_ProvidesQuoteInPage()
    {
        // Arrange
        var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IQuoteService, TestQuoteService>();
                });
            })
            .CreateClient();

        //Act
        var response = await client.GetAsync("/quote");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal("Something's interfering with time, Mr. Scarman, " +
            "and time is my business.", content);
    }
}
