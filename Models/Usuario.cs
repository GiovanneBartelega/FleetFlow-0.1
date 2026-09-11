using System.ComponentModel.DataAnnotations;

namespace FleetFlow.Api.Models;

public class Usuario
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(500)]
    public string SenhaHash { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Perfil { get; set; } = "Administrador";

    public bool Ativo { get; set; } = true;
}
