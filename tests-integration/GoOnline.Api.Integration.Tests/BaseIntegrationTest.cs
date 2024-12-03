namespace GoOnline.Api.Integration.Tests;

public abstract class BaseIntegrationTest : IClassFixture<GoOnlineWebApplicationFactory>
{
    protected abstract string URL { get; }
    protected HttpClient Client { get; }

    protected BaseIntegrationTest(GoOnlineWebApplicationFactory factory)
    {
        Client = factory.CreateClient();
        Client.BaseAddress = new Uri("https://localhost:7099/api/");
    }
}
