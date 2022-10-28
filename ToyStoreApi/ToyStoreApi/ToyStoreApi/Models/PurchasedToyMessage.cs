namespace ToyStoreApi.Models;

public class PurchasedToyMessage
{
    public string UPC { get; set; }
    public string ToyName { get; set; }
    public string PurchaserEmail { get; set; }
    public Guid ChildId { get; set; }
}