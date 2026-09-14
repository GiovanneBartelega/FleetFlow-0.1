using System.Collections.ObjectModel;
using FleetFlow.Mobile.Models;
using FleetFlow.Mobile.Services;

namespace FleetFlow.Mobile.ViewModels;

/// <summary>
/// Item da lista de perfis: é o Perfil original + o emoji (ícone) pronto
/// para a tela. Criamos esta classe auxiliar para o XAML não precisar
/// de nenhuma lógica para escolher o emoji.
/// </summary>
public class PerfilItem
{
    // Perfil original vindo do serviço mock
    public Perfil Dados { get; }

    // Nome do perfil (atalho para Dados.Nome)
    public string Nome => Dados.Nome;

    // Descrição do perfil (atalho para Dados.Descricao)
    public string Descricao => Dados.Descricao;

    // Quantidade de usuários vinculados (atalho para Dados.QuantidadeUsuarios)
    public int QuantidadeUsuarios => Dados.QuantidadeUsuarios;

    // Emoji exibido no cartão, escolhido de acordo com o nome do perfil
    public string Icone { get; }

    public PerfilItem(Perfil perfil)
    {
        Dados = perfil;

        // Escolhe o emoji conforme o nome do perfil
        Icone = perfil.Nome switch
        {
            "Administrador" => "⭐",
            "Gestor de Frota" => "🚚",
            "Financeiro" => "💰",
            "Motorista" => "🚗",
            _ => "👤" // ícone genérico caso apareça um perfil novo
        };
    }
}

/// <summary>
/// ViewModel da tela de Perfis de Usuário.
/// Carrega a lista de perfis do serviço mock e prepara o emoji de cada um.
/// </summary>
public class PerfisViewModel : BaseViewModel
{
    /// <summary>
    /// Lista de perfis exibida na CollectionView da tela.
    /// </summary>
    public ObservableCollection<PerfilItem> Perfis { get; }

    public PerfisViewModel()
    {
        // Busca os 4 perfis fixos do serviço mock e "embrulha" cada um
        // em um PerfilItem, que já traz o emoji correto
        var perfisDoMock = MockDataService.ObterPerfis();

        Perfis = new ObservableCollection<PerfilItem>();

        foreach (var perfil in perfisDoMock)
        {
            Perfis.Add(new PerfilItem(perfil));
        }
    }
}
