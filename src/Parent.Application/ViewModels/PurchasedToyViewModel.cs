namespace Parent.Application.MessageModels;

public class PurchasedToyViewModel
{
    public string UPC { get; set; }
    public string ToyName { get; set; }
    public Guid ChildId { get; set; }
}