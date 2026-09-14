namespace FleetFlow.Mobile.Models;

/// <summary>
/// Representa um perfil de usuário do sistema FleetFlow.
/// Cada perfil define o que o usuário pode acessar.
/// Exemplo: Administrador, Gestor de Frota, Financeiro, Motorista.
/// </summary>
public class Perfil
{
    // Identificador único do perfil
    public int Id { get; set; }

    // Nome exibido na tela, ex.: "Gestor de Frota"
    public string Nome { get; set; } = "";

    // Texto curto explicando o que o perfil pode fazer no sistema
    public string Descricao { get; set; } = "";

    // Quantidade de usuários vinculados a este perfil
    public int QuantidadeUsuarios { get; set; }
}
