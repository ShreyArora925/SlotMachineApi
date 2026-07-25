using System.ComponentModel.DataAnnotations;

namespace SlotMachineApi.Application.Models;

public class UpdateBalanceRequest
{
    [Required]
    public string PlayerId { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
}