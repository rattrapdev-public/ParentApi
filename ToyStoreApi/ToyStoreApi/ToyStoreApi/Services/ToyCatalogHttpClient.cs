using ToyStoreApi.Models;

namespace ToyStoreApi.Services;

public class ToyCatalogHttpClient : IToyCatalogHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ToyCatalogHttpClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ToyModel> GetByUpc(string upc)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var result = await httpClient.GetFromJsonAsync<ToyModel>($"https://localhost:7155/api/toys/{upc}");

        if (result is null)
        {
            throw new ToyNullException(upc);
        }
        
        return result;
    }
}