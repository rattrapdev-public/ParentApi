using System.Collections.ObjectModel;
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
        var dto = await _parentDbContext.Children.FirstOrDefaultAsync(x => x.ChildId == childIdentifier.Id);

        if (dto == null)
        {
            throw new NotFoundException($"Child {childIdentifier.Id} was not found");
        }
        
        return Child.Reconstitute(new ChildIdentifier(dto.ChildId), new GuardianIdentifier(dto.GuardianId), new Name(dto.FirstName, dto.LastName), ToyBox.Empty());
    }

    public async Task Store(Child child)
    {
        var dto = new ChildDto
        {
            ChildId = child.Identifier.Id,
            GuardianId = child.GuardianIdentifier.Id,
            FirstName = child.Name.FirstName,
            LastName = child.Name.LastName,
            Toys = new Collection<ToyDto>(child.ToyBox
                .Select(x => new ToyDto {ChildId = child.Identifier.Id, Upc = x.Upc}).ToList())
        };

        await _parentDbContext.Children.AddAsync(dto);
    }

    public async Task<IEnumerable<Child>> GetBy(GuardianIdentifier guardianIdentifier)
    {
        var dtos = await _parentDbContext.Children.Where(x => x.GuardianId == guardianIdentifier.Id).ToListAsync();

        return dtos.Select(dto => Child.Reconstitute(new ChildIdentifier(dto.ChildId), new GuardianIdentifier(dto.GuardianId), new Name(dto.FirstName, dto.LastName), ToyBox.Empty())).ToList();
    }

    public async Task<IEnumerable<Child>> All()
    {
        var dtos = await _parentDbContext.Children.ToListAsync();

        return dtos.Select(dto => Child.Reconstitute(new ChildIdentifier(dto.ChildId), new GuardianIdentifier(dto.GuardianId), new Name(dto.FirstName, dto.LastName), ToyBox.Empty())).ToList();
    }
}