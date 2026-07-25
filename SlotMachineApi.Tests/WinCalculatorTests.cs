using SlotMachineApi.Application.Services;
using SlotMachineApi.Domain;

namespace SlotMachineApi.Tests;

public class WinCalculatorTests
{
    [Theory]
    [InlineData(new[] { 3, 3, 3, 4, 5 }, 1, 9)]
    [InlineData(new[] { 2, 3, 2 }, 1, 0)]
    [InlineData(new[] { 7, 7, 7, 3, 7, 7, 3 }, 1, 21)]
    public void CalculateLineWin_ReturnsExpectedWin(int[] line, decimal bet, decimal expected)
    {
        var result = WinCalculator.CalculateLineWin(line, bet);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateTotalWin_FullMatrix_Returns27TimesBet()
    {
        var matrix = new int[][]
        {
            new[] { 3, 3, 3, 4, 5 },
            new[] { 2, 3, 2, 3, 3 },
            new[] { 1, 2, 3, 3, 3 }
        };

        var winLines = new List<WinLine>
        {
            new WinLine { Type = "StraightRow", StartRow = 0 },
            new WinLine { Type = "StraightRow", StartRow = 1 },
            new WinLine { Type = "StraightRow", StartRow = 2 },
            new WinLine { Type = "ZigzagDiagonal", StartRow = 0 },
            new WinLine { Type = "ZigzagDiagonal", StartRow = 1 },
            new WinLine { Type = "ZigzagDiagonal", StartRow = 2 }
        };

        var result = WinCalculator.Calculate(matrix, winLines, 1);
        Assert.Equal(27, result);
    }
}