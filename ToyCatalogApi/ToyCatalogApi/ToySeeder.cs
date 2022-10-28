using ToyCatalogApi.Models;

namespace ToyCatalogApi;

public static class ToySeeder
{
    public static void SeedToys(ToyDbContext context)
    {
        if (!context.Toys.Any())
        {
            var countries = new List<ToyModel>
            {
                new ToyModel { UPC = "9501212345", Cost = 15, Name = "He-Man", Description = "Muscular Action Figure" },
                new ToyModel { UPC = "9501212346", Cost = 299.99m, Name = "Nintendo Switch", Description = "Portable console by Nintendo" },
                new ToyModel { UPC = "9501212347", Cost = 3.99m, Name = "Dominaria Booster Pack", Description = "Magic the Gathering Dominaria Draft Booster Park" },
                new ToyModel { UPC = "9501212348", Cost = 29.99m, Name = "Nerf Bazooka", Description = "Nerf Mega Bazooka" },
                new ToyModel { UPC = "9501212349", Cost = 99.99m, Name = "Lego Harry Potter Castle", Description = "Harry Potter Hogwarts Castle Complete Set" },
                new ToyModel { UPC = "9501212350", Cost = 59.99m, Name = "Mario Odyssey", Description = "Mario Odyssey Physical Game Cart" },
                new ToyModel { UPC = "9501212351", Cost = 10, Name = "Basketball", Description = "29.5 Regulation Basketball" },
            };
            context.AddRange(countries);
            context.SaveChanges();
        }
    }
}