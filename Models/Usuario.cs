using System.ComponentModel.DataAnnotations;

namespace FleetFlow.Api.Models;

public class Usuario
{
    public Guid Id { get; set; }
    public Guid PerfilId { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? GoogleSubjectId { get; set; }
    public string StatusAprovacao { get; set; } = "AGUARDANDO_APROVACAO";
    public string? FotoUrl { get; set; }
    public DateTime? UltimoLoginEm { get; set; }
    public bool Ativo { get; set; } = true;
    public Perfil? Perfil { get; set; }
}