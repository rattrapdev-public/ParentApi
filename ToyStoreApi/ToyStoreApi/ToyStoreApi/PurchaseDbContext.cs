using Microsoft.EntityFrameworkCore;
using ToyStoreApi.Models;

namespace ToyStoreApi;

public class PurchaseDbContext : DbContext
{
    public DbSet<PurchaseModel> Purchases { get; set; }

    public PurchaseDbContext(DbContextOptions<PurchaseDbContext> options)
        : base(options)
    {
    }
}