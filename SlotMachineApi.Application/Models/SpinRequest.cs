using System.ComponentModel.DataAnnotations;

namespace SlotMachineApi.Application.Models;

public class SpinRequest
{
    [Required]
    public string PlayerId { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Bet amount must be greater than zero.")]
    public decimal BetAmount { get; set; }
}