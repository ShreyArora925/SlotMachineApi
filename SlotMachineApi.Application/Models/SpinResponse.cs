namespace SlotMachineApi.Application.Models;

public class SpinResponse
{
    public int[][] ResultMatrix { get; set; } = Array.Empty<int[]>();
    public decimal WinAmount { get; set; }
    public decimal CurrentBalance { get; set; }
}