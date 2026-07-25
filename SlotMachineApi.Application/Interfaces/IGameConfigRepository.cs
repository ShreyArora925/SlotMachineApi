using SlotMachineApi.Domain;

namespace SlotMachineApi.Application.Interfaces;

public interface IGameConfigRepository
{
    Task<GameConfig> GetConfigAsync();
    Task SeedConfigAsync();

}