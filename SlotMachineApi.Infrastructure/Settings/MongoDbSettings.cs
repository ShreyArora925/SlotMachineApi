namespace SlotMachineApi.Infrastructure.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string PlayersCollectionName { get; set; } = string.Empty;
    public string GameConfigCollectionName { get; set; } = string.Empty;
}