namespace ToyCatalogApi.Models;

public class ToyNameChangedEvent
{
    public string UPC { get; set; }
    public string UpdatedName { get; set; }
}