using Microsoft.EntityFrameworkCore;
using Parent.Domain;

namespace Parent.Persistence;

public class ChildRepository : IChildRepository
{
    private readonly ParentDbContext _parentDbContext;

    public ChildRepository(ParentDbContext parentDbContext)
    {
        _parentDbContext = parentDbContext;
    }
    
    public async Task<Child> GetBy(ChildIdentifier childIdentifier)
    {
        var child = await _parentDbContext.Children.FirstOrDefaultAsync(x => x.Identifier.Id == childIdentifier.Id);

        if (child == null)
        {
            throw new NotFoundException($"Child {childIdentifier.Id} was not found");
        }

        return child;
    }

    public async Task Store(Child child)
    {
        await _parentDbContext.Children.AddAsync(child);
    }

    public async Task<IEnumerable<Child>> GetBy(GuardianIdentifier guardianIdentifier)
    {
        var children = await _parentDbContext.Children.Where(x => x.GuardianIdentifier.Id == guardianIdentifier.Id).ToListAsync();

        return children;
    }

    public async Task<IEnumerable<Child>> All()
    {
        var children = await _parentDbContext.Children.ToListAsync();

        return children;
    }
}