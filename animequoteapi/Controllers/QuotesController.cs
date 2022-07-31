using Microsoft.AspNetCore.Mvc;

namespace AnimeQuoteApi.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotesController : ControllerBase
{
    private readonly QuoteClient _client;

    public QuotesController(QuoteClient client)
    {
        _client = client;
    }

    [HttpGet()]
    public Task<Quote> Get()
    {
        return _client.Get();
    }
}
