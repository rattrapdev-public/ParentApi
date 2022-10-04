namespace Parent.Application.ViewModels;

public class ChildViewModel
{
    public Guid ChildId { get; set; }
    public Guid GuardianId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<ToyViewModel> Toys { get; set; }
}