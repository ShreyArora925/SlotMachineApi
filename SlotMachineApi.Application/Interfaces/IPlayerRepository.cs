using SlotMachineApi.Domain;

namespace SlotMachineApi.Application.Interfaces;

public interface IPlayerRepository
{
    Task<Player> GetByPlayerIdAsync(string playerId);
    Task<Player> DeductBalanceAsync(string playerId, decimal amount);
    Task<Player> AddBalanceAsync(string playerId, decimal amount);
    Task SeedPlayerAsync();
}