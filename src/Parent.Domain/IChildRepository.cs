namespace Parent.Domain;

public interface IChildRepository
{
    Task<Child> GetBy(ChildIdentifier childIdentifier);
    Task Store(Child child);
    Task<IEnumerable<Child>> GetBy(GuardianIdentifier guardianIdentifier);
    Task<IEnumerable<Child>> All();
}