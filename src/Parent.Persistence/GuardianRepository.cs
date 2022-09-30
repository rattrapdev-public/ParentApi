using Microsoft.EntityFrameworkCore;
using Parent.Domain;

namespace Parent.Persistence;

public class GuardianRepository : IGuardianRepository
{
    private readonly GuardianDbContext _context;

    public GuardianRepository(GuardianDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guardian> GetBy(GuardianIdentifier guardianIdentifier)
    {
        var dto = await _context.Guardians.FirstOrDefaultAsync(x => x.GuardianId == guardianIdentifier.Id);

        if (dto == null)
        {
            throw new NotFoundException($"Guardian {guardianIdentifier.Id} was not found");
        }

        return new Guardian(
            guardianIdentifier,
            new Name(dto.FirstName, dto.LastName),
            new EmailAddress(dto.EmailAddress),
            new Address(dto.AddressLine1, dto.AddressLine2, dto.City, dto.State, dto.Zip));
    }

    public async Task Store(Guardian guardian)
    {
        var dto = new GuardianDto
        {
            GuardianId = guardian.Id,
            FirstName = guardian.Name.FirstName,
            LastName = guardian.Name.LastName,
            EmailAddress = guardian.Email.Email,
            AddressLine1 = guardian.Address.Address1,
            AddressLine2 = guardian.Address.Address2,
            City = guardian.Address.City,
            State = guardian.Address.State,
            Zip = guardian.Address.Zip,
        };

        await _context.Guardians.AddAsync(dto);
    }

    public async Task<IEnumerable<Guardian>> All()
    {
        var dtos = await _context.Guardians.ToListAsync();
        var guardians = new List<Guardian>();

        foreach (var dto in dtos)
        {
            var guardian = new Guardian(
                new GuardianIdentifier(dto.GuardianId),
                new Name(dto.FirstName, dto.LastName),
                new EmailAddress(dto.EmailAddress),
                new Address(dto.AddressLine1, dto.AddressLine2, dto.City, dto.State, dto.Zip));
            guardians.Add(guardian);
        }

        return guardians;
    }
}