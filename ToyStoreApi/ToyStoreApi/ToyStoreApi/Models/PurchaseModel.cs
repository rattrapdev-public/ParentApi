using System.ComponentModel.DataAnnotations;

namespace ToyStoreApi.Models;

public class PurchaseModel
{
    [Key]
    public string UPC { get; set; }
    public decimal Cost { get; set; }
    public string PurchaserEmail { get; set; }
    public Guid ChildId { get; set; }
}