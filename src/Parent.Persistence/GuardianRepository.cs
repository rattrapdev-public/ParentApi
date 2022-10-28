using Microsoft.EntityFrameworkCore;
using Parent.Domain;

namespace Parent.Persistence;

public class GuardianRepository : IGuardianRepository
{
    private readonly ParentDbContext _context;

    public GuardianRepository(ParentDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guardian> GetBy(GuardianIdentifier guardianIdentifier)
    {
        var guardian = await _context.Guardians.FirstOrDefaultAsync(x => x.Id == guardianIdentifier.Id);

        if (guardian == null)
        {
            throw new NotFoundException($"Guardian {guardianIdentifier.Id} was not found");
        }

        return guardian;
    }

    public async Task Store(Guardian guardian)
    {
        if (_context.Guardians.AsEnumerable().Any(x => x.Identifier.Id == guardian.Identifier.Id))
        {
            _context.Guardians.Update(guardian);

            return;
        }
        
        await _context.Guardians.AddAsync(guardian);
    }

    public async Task<IEnumerable<Guardian>> All()
    {
        var guardians = await _context.Guardians.ToListAsync();

        return guardians;
    }
}