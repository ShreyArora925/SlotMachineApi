using Moq;
using SlotMachineApi.Application.Interfaces;
using SlotMachineApi.Application.Models;
using SlotMachineApi.Application.Services;
using SlotMachineApi.Domain;

namespace SlotMachineApi.Tests;

public class GameServiceTests
{
    private readonly Mock<IPlayerRepository> _playerRepoMock;
    private readonly Mock<IGameConfigRepository> _configRepoMock;
    private readonly GameService _gameService;

    public GameServiceTests()
    {
        _playerRepoMock = new Mock<IPlayerRepository>();
        _configRepoMock = new Mock<IGameConfigRepository>();
        _gameService = new GameService(_playerRepoMock.Object, _configRepoMock.Object);
    }

    [Fact]
    public async Task Spin_InsufficientBalance_ThrowsException()
    {
        _configRepoMock.Setup(x => x.GetConfigAsync()).ReturnsAsync(new GameConfig
        {
            MatrixWidth = 5,
            MatrixHeight = 3,
            WinLines = new List<WinLine>
            {
                new WinLine { Type = "StraightRow", StartRow = 0 }
            }
        });

        _playerRepoMock.Setup(x => x.DeductBalanceAsync(It.IsAny<string>(), It.IsAny<decimal>()))
            .ReturnsAsync((Player)null!);

        var request = new SpinRequest { PlayerId = "player1", BetAmount = 100 };

        await Assert.ThrowsAsync<InvalidOperationException>(() => _gameService.SpinAsync(request));
    }

    [Fact]
    public async Task UpdateBalance_ValidRequest_ReturnsNewBalance()
    {
        _playerRepoMock.Setup(x => x.AddBalanceAsync("player1", 100))
            .ReturnsAsync(new Player { PlayerId = "player1", Balance = 200 });

        var request = new UpdateBalanceRequest { PlayerId = "player1", Amount = 100 };
        var result = await _gameService.UpdateBalanceAsync(request);

        Assert.Equal(200, result.CurrentBalance);
    }
}