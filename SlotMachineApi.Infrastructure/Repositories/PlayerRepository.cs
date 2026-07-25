using MongoDB.Driver;
using SlotMachineApi.Application.Interfaces;
using SlotMachineApi.Domain;
using SlotMachineApi.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace SlotMachineApi.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly IMongoCollection<Player> _collection;

    public PlayerRepository(IOptions<MongoDbSettings> settings, IMongoClient mongoClient)
    {
        var db = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _collection = db.GetCollection<Player>(settings.Value.PlayersCollectionName);
    }

    public async Task<Player> GetByPlayerIdAsync(string playerId)
    {
        return await _collection
            .Find(p => p.PlayerId == playerId)
            .FirstOrDefaultAsync();
    }

    public async Task<Player> DeductBalanceAsync(string playerId, decimal amount)
    {
        var filter = Builders<Player>.Filter.And(
            Builders<Player>.Filter.Eq(p => p.PlayerId, playerId),
            Builders<Player>.Filter.Gte(p => p.Balance, amount)
        );
        var update = Builders<Player>.Update.Inc(p => p.Balance, -amount);
        var options = new FindOneAndUpdateOptions<Player>
        {
            ReturnDocument = ReturnDocument.After
        };
        return await _collection.FindOneAndUpdateAsync(filter, update, options);
    }

    public async Task<Player> AddBalanceAsync(string playerId, decimal amount)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.PlayerId, playerId);
        var update = Builders<Player>.Update
            .Inc(p => p.Balance, amount)
            .SetOnInsert(p => p.PlayerId, playerId);
        var options = new FindOneAndUpdateOptions<Player>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = true
        };
        return await _collection.FindOneAndUpdateAsync(filter, update, options);
    }

    public async Task SeedPlayerAsync()
    {
        var existing = await _collection.Find(p => p.PlayerId == "player1").FirstOrDefaultAsync();
        if (existing == null)
        {
            await _collection.InsertOneAsync(new Player
            {
                PlayerId = "player1",
                Balance = 1000
            });
        }
    }

}