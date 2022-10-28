using ToyStoreApi.Models;

namespace ToyStoreApi.Services;

public interface IToyCatalogHttpClient
{
    Task<ToyModel> GetByUpc(string upc);
}