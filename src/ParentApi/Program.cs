using MediatR;
using Microsoft.EntityFrameworkCore;
using Parent.Application;
using Parent.Domain;
using Parent.Domain.Events;
using Parent.Persistence;
using IPublisher = Parent.Persistence.IPublisher;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ParentDbContext>(options =>
{
   options.UseSqlite(@"Data Source = test.db");
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPublisher, Publisher>();
builder.Services.AddScoped<IGuardianRepository, GuardianRepository>();
builder.Services.AddScoped<ICreateGuardian, CreateGuardian>();
builder.Services.AddScoped<IGetAllGuardians, GetAllGuardians>();
builder.Services.AddScoped<IGetGuardianById, GetGuardianById>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<INotificationHandler<GuardianCreatedEvent>, GuardianCreatedConsoleService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
   var guardianDbContext = scope.ServiceProvider.GetRequiredService<ParentDbContext>();
   
   guardianDbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();