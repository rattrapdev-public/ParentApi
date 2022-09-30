namespace Parent.Domain;

public interface IGuardianRepository
{
    Task<Guardian> GetBy(GuardianIdentifier guardianIdentifier);
    Task Store(Guardian guardian);
    Task<IEnumerable<Guardian>> All();
}