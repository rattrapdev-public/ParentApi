using Microsoft.EntityFrameworkCore;

namespace Parent.Persistence;

public class GuardianDbContext : DbContext
{
    public DbSet<GuardianDto> Guardians { get; set; }

    public GuardianDbContext(DbContextOptions<GuardianDbContext> options)
        : base(options)
    {
        
    }
}