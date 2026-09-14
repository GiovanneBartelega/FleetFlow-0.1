namespace FleetFlow.Mobile.Models;

/// <summary>
/// Usuário fixo (mock) usado em todo o aplicativo.
/// Como não há integração com API real neste projeto acadêmico,
/// os dados abaixo simulam o usuário logado no sistema.
/// </summary>
public class UsuarioMock
{
    // Nome completo do usuário
    public string Nome { get; set; } = "João Pedro Silva";

    // Primeiro nome, usado na saudação da tela inicial ("Olá, João Pedro")
    public string PrimeiroNome { get; set; } = "João Pedro";

    // E-mail exibido na tela de perfil
    public string Email { get; set; } = "joaopedro@jpph.com.br";

    // Iniciais exibidas no avatar circular (ex.: "JP")
    public string Iniciais { get; set; } = "JP";

    // Perfil de acesso ativo do usuário no sistema
    public string PerfilAtivo { get; set; } = "Administrador";

    // Nome da empresa transportadora
    public string Empresa { get; set; } = "JP & PH Transportes";
}
