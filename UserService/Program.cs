using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.AddSqlServerDbContext<UserServiceDbContext>("userdb");

builder.Services.AddScoped<IUserService, UserServiceImplementation>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var serviceScope = app.Services.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<UserServiceDbContext>();
    await dbContext.Database.MigrateAsync();

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();