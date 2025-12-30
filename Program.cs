using API_FTN_V1._0.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Ajouter EF Core avec la chaîne de connexion
builder.Services.AddDbContext<UserApiContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("AuthConnection"), // La chaîne de connexion
        new MySqlServerVersion(new Version(8, 0, 2)) // Remplacez par votre version de MySQL
    )
);
// Pour MySQL, utilisez : options.UseMySql

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
