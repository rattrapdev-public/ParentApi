using Microsoft.EntityFrameworkCore;
using ToyStoreApi;
using ToyStoreApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IToyCatalogCache, ToyCatalogCache>();
builder.Services.AddTransient<IToyCatalogHttpClient, ToyCatalogHttpClient>();
builder.Services.AddDbContext<PurchaseDbContext>(options =>
{
    options.UseSqlite(@"Data source = purchase.db");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var purchaseDbContext = scope.ServiceProvider.GetRequiredService<PurchaseDbContext>();
    
    purchaseDbContext.Database.Migrate();
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