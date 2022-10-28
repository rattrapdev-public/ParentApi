using System.ComponentModel.DataAnnotations;

namespace ToyCatalogApi.Models;

public class ToyModel
{
    [Key]
    public string UPC { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
}