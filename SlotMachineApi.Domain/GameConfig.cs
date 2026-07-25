using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SlotMachineApi.Domain;

public class GameConfig
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("matrixWidth")]
    public int MatrixWidth { get; set; }

    [BsonElement("matrixHeight")]
    public int MatrixHeight { get; set; }

    [BsonElement("winLines")]
    public List<WinLine> WinLines { get; set; } = new();
}

public class WinLine
{
    [BsonElement("type")]
    public string Type { get; set; } = string.Empty;

    [BsonElement("startRow")]
    public int StartRow { get; set; }
}