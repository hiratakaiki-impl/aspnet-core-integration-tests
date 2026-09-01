using System.Net;
using System.Net.Http.Json;

namespace TodoApi.IntegrationTests;

public class TodoApiTests(
    CustomWebApplicationFactory<Program> factory) :
    IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
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
}
