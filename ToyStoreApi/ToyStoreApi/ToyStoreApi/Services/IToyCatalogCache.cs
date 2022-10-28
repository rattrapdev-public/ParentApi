using ToyStoreApi.Models;

namespace ToyStoreApi.Services;

public interface IToyCatalogCache
{
    Task<ToyModel> GetByUpc(string upc);
}