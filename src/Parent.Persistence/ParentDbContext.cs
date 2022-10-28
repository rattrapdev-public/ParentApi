using Microsoft.EntityFrameworkCore;
using Parent.Domain;

namespace Parent.Persistence;

public class ParentDbContext : DbContext
{
    public DbSet<Guardian> Guardians { get; set; }
    public DbSet<Child> Children { get; set; }

    public ParentDbContext(DbContextOptions<ParentDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guardian>().HasKey(x => x.Identifier);
        modelBuilder.Entity<Guardian>().Property(x => x.Identifier)
            .HasConversion(y => y.Id, z => new GuardianIdentifier(z))
            .HasColumnName("GuardianId")
            .IsRequired();
        modelBuilder.Entity<Guardian>().OwnsOne(x => x.Address, sb =>
        {
            sb.Property(a => a.Address1).HasColumnName("AddressLine1").HasColumnType("TEXT");
            sb.Property(a => a.Address2).HasColumnName("AddressLine2").HasColumnType("TEXT");
            sb.Property(a => a.City).HasColumnName("City").HasColumnType("TEXT");
            sb.Property(a => a.State).HasColumnName("State").HasColumnType("TEXT");
            sb.Property(a => a.Zip).HasColumnName("Zip").HasColumnType("TEXT");
        });
        modelBuilder.Entity<Guardian>().OwnsOne(x => x.Name, sb =>
        {
            sb.Property(n => n.FirstName).HasColumnName("FirstName").HasColumnType("TEXT");
            sb.Property(n => n.LastName).HasColumnName("LastName").HasColumnType("TEXT");
        });
        modelBuilder.Entity<Guardian>().OwnsOne(x => x.Email, sb =>
        {
            sb.Property(e => e.Email).HasColumnName("Email").HasColumnType("TEXT");
        });

        modelBuilder.Entity<Child>().HasKey(x => x.Identifier);
        modelBuilder.Entity<Child>().Property(x => x.Identifier)
            .HasConversion(y => y.Id, z => new ChildIdentifier(z))
            .HasColumnName("ChildId");
        modelBuilder.Entity<Child>().HasOne<Guardian>().WithMany().HasForeignKey(x => x.GuardianIdentifier);
        modelBuilder.Entity<Child>().Property(x => x.GuardianIdentifier)
            .HasConversion(y => y.Id, z => new GuardianIdentifier(z));
        modelBuilder.Entity<Child>().OwnsOne(x => x.Name, sb =>
        {
            sb.Property(n => n.FirstName).HasColumnName("FirstName").HasColumnType("TEXT");
            sb.Property(n => n.LastName).HasColumnName("LastName").HasColumnType("TEXT");
        });
        modelBuilder.Entity<Child>().OwnsMany(x => x.ToyBox, sb =>
        {
            sb.WithOwner().HasForeignKey("ChildId");
            sb.HasKey(t => t.Upc);
            sb.ToTable("ToyBox");
            sb.Property(t => t.Name).HasColumnName("ToyName");
            sb.Property(t => t.Upc).HasColumnName("Upc");
        });
    }
}