using FleetFlow.Mobile.Models;
using FleetFlow.Mobile.Services;

namespace FleetFlow.Mobile.ViewModels;

/// <summary>
/// ViewModel da tela inicial (HomePage).
/// Expõe os dados do usuário logado e os números dos cartões de KPI.
/// Todos os valores são fixos (mock) — não há cálculo nem API real.
/// </summary>
public class HomeViewModel : BaseViewModel
{
    // Usuário logado, obtido do serviço de dados falsos
    private readonly UsuarioMock _usuario;

    public HomeViewModel()
    {
        // Carrega os dados do usuário assim que o ViewModel é criado
        _usuario = MockDataService.ObterUsuario();
    }

    // --- Propriedades usadas no bloco de identificação da tela ---

    // Texto de saudação, ex.: "Olá, João Pedro"
    public string Saudacao => $"Olá, {_usuario.PrimeiroNome}";

    // Linha abaixo da saudação, ex.: "Administrador • JP & PH Transportes"
    public string Identificacao => $"{_usuario.PerfilAtivo} • {_usuario.Empresa}";

    // Iniciais exibidas no avatar circular do topo, ex.: "JP"
    public string Iniciais => _usuario.Iniciais;

    // --- Valores dos 4 cartões de KPI (valores fixos de exemplo) ---

    public string SaldoAtual => "R$ 37.071,04";
    public string VariacaoSaldo => "▲ 12,4%";
    public string AReceber => "R$ 18.400,00";
    public string APagar => "R$ 42.150,32";
    public string EmViagem => "1";

    // Texto do cartão de insight destacado
    public string TextoInsight =>
        "Sua rota Sete Lagoas/Santana gerou R$ 12.400 em 7 viagens este mês. Rota mais lucrativa.";
}
