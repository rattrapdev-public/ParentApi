using Microsoft.EntityFrameworkCore;

namespace Parent.Persistence;

public class ParentDbContext : DbContext
{
    public DbSet<GuardianDto> Guardians { get; set; }
    public DbSet<ChildDto> Children { get; set; }
    public DbSet<ToyDto> Toys { get; set; }

    public ParentDbContext(DbContextOptions<ParentDbContext> options)
        : base(options)
    {
        
    }
}