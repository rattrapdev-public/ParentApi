using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parent.Persistence;

[Table("Toys")]
public class ToyDto
{
    [Key]
    public string Upc { get; set; }
    
    [ForeignKey("Children")]
    public Guid ChildId { get; set; }
}