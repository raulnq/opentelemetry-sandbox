using System.Text.Json;

namespace AnimeQuoteApi;

public class QuoteClient
{
    private readonly HttpClient _httpClient;

    public QuoteClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Quote> Get()
    {
        var httpResponse = await _httpClient.GetAsync("/api/random");

        httpResponse.EnsureSuccessStatusCode();

        var body = JsonSerializer.Deserialize<Quote>(await httpResponse.Content.ReadAsStringAsync());

        return body!;
    }
}
