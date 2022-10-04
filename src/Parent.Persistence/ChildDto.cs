using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parent.Persistence;

[Table("Children")]
public class ChildDto
{
    [Key]
    public Guid ChildId { get; set; }
    
    [ForeignKey("Guardians")]
    public Guid GuardianId { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public Collection<ToyDto> Toys { get; set; }
}