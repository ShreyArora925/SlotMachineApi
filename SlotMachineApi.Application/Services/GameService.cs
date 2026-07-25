using SlotMachineApi.Application.Interfaces;
using SlotMachineApi.Application.Models;
using SlotMachineApi.Domain;

namespace SlotMachineApi.Application.Services;

public class GameService : IGameService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameConfigRepository _gameConfigRepository;

    public GameService(IPlayerRepository playerRepository, IGameConfigRepository gameConfigRepository)
    {
        _playerRepository = playerRepository;
        _gameConfigRepository = gameConfigRepository;
    }

    public async Task<SpinResponse> SpinAsync(SpinRequest request)
    {
        if (request.BetAmount <= 0)
            throw new InvalidOperationException("Bet amount must be greater than zero.");

       
        var config = await _gameConfigRepository.GetConfigAsync();
        var player = await _playerRepository.DeductBalanceAsync(request.PlayerId, request.BetAmount);
        if (player == null)
            throw new InvalidOperationException("Insufficient balance or player not found.");

        var matrix = GenerateMatrix(config.MatrixWidth, config.MatrixHeight);
        var winAmount = WinCalculator.Calculate(matrix, config.WinLines, request.BetAmount);

        if (winAmount > 0)
            player = await _playerRepository.AddBalanceAsync(request.PlayerId, winAmount);

        return new SpinResponse
        {
            ResultMatrix = matrix,
            WinAmount = winAmount,
            CurrentBalance = player.Balance
        };
    }

    public async Task<UpdateBalanceResponse> UpdateBalanceAsync(UpdateBalanceRequest request)
    {
        if (request.Amount <= 0)
            throw new InvalidOperationException("Amount must be greater than zero.");

        var player = await _playerRepository.AddBalanceAsync(request.PlayerId, request.Amount);
        if (player == null)
            throw new InvalidOperationException("Player not found.");

        return new UpdateBalanceResponse
        {
            CurrentBalance = player.Balance
        };
    }

    private int[][] GenerateMatrix(int width, int height)
    {
        var random = new Random();
        var matrix = new int[height][];

        for (int row = 0; row < height; row++)
        {
            matrix[row] = new int[width];
            for (int col = 0; col < width; col++)
                matrix[row][col] = random.Next(0, 10);
        }

        return matrix;
    }

    public async Task<string> SeedAsync()
    {
        await _playerRepository.SeedPlayerAsync();
        await _gameConfigRepository.SeedConfigAsync();
        return "Database seeded successfully.";
    }
}