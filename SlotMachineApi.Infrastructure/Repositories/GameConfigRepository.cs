using MongoDB.Driver;
using SlotMachineApi.Application.Interfaces;
using SlotMachineApi.Domain;
using SlotMachineApi.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace SlotMachineApi.Infrastructure.Repositories;

public class GameConfigRepository : IGameConfigRepository
{
    private readonly IMongoCollection<GameConfig> _collection;

    public GameConfigRepository(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = db.GetCollection<GameConfig>(settings.Value.GameConfigCollectionName);
    }

    public async Task<GameConfig> GetConfigAsync()
    {
        return await _collection
            .Find(_ => true)
            .FirstOrDefaultAsync();
    }

    public async Task SeedConfigAsync()
    {
        var existing = await _collection.Find(_ => true).FirstOrDefaultAsync();
        if (existing == null)
        {
            await _collection.InsertOneAsync(new GameConfig
            {
                MatrixWidth = 5,
                MatrixHeight = 3,
                WinLines = new List<WinLine>
            {
                new WinLine { Type = "StraightRow", StartRow = 0 },
                new WinLine { Type = "StraightRow", StartRow = 1 },
                new WinLine { Type = "StraightRow", StartRow = 2 },
                new WinLine { Type = "ZigzagDiagonal", StartRow = 0 },
                new WinLine { Type = "ZigzagDiagonal", StartRow = 1 },
                new WinLine { Type = "ZigzagDiagonal", StartRow = 2 }
            }
            });
        }
    }
}