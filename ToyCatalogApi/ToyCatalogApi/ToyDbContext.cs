using Microsoft.EntityFrameworkCore;
using ToyCatalogApi.Models;

namespace ToyCatalogApi;

public class ToyDbContext : DbContext
{
    public DbSet<ToyModel> Toys { get; set; }

    public ToyDbContext(DbContextOptions<ToyDbContext> options)
        : base(options)
    {
    }
}