using SlotMachineApi.Application.Models;

namespace SlotMachineApi.Application.Interfaces;

public interface IGameService
{
    Task<string> SeedAsync();
    Task<SpinResponse> SpinAsync(SpinRequest request);
    Task<UpdateBalanceResponse> UpdateBalanceAsync(UpdateBalanceRequest request);
}