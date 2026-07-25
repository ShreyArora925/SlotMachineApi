using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Scalar.AspNetCore;
using SlotMachineApi.Application.Interfaces;
using SlotMachineApi.Application.Services;
using SlotMachineApi.Infrastructure.Repositories;
using SlotMachineApi.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Settings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// MongoDB Client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Repositories
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IGameConfigRepository, GameConfigRepository>();

// Services
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();