namespace ToyStoreApi.Services;

public class ToyNullException : Exception
{
    public ToyNullException(string upc) : base($"Toy {upc} does not exist in the catalog.")
    {
        
    }
}