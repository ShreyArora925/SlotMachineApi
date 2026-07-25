using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SlotMachineApi.Domain;

public class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("playerId")]
    public string PlayerId { get; set; } = string.Empty;

    [BsonElement("balance")]
    public decimal Balance { get; set; }
}