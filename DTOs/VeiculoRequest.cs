using System.ComponentModel.DataAnnotations;

namespace FleetFlow.Api.DTOs;

public class VeiculoRequest
{
    [Required, MaxLength(10)]
    public string Placa { get; set; } = string.Empty;

    [Required, MaxLength(80)]
    public string Modelo { get; set; } = string.Empty;

    [Required, MaxLength(80)]
    public string Marca { get; set; } = string.Empty;

    [Required, MaxLength(30)]
    public string Status { get; set; } = "Disponível";

    public int? AnoFabricacao { get; set; }
    public decimal? CapacidadeCarga { get; set; }
}
