using Microsoft.EntityFrameworkCore;
using ToyCatalogApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToyDbContext>(options =>
{
    options.UseSqlite(@"Data source = catalog.db");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var toyDbContext = scope.ServiceProvider.GetRequiredService<ToyDbContext>();
    
    toyDbContext.Database.Migrate();
    
    ToySeeder.SeedToys(toyDbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();