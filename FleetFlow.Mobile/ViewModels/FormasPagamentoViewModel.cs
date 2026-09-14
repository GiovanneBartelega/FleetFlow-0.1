using System.Collections.ObjectModel;
using FleetFlow.Mobile.Models;
using FleetFlow.Mobile.Services;

namespace FleetFlow.Mobile.ViewModels;

/// <summary>
/// ViewModel da tela de Formas de Pagamento.
/// Carrega a lista de formas de pagamento do serviço mock.
///
/// O Switch de cada item é ligado/desligado diretamente na propriedade
/// "Ativo" do modelo (binding TwoWay), sem lógica adicional — é só visual.
/// </summary>
public class FormasPagamentoViewModel : BaseViewModel
{
    /// <summary>
    /// Lista de formas de pagamento exibida na CollectionView da tela.
    /// </summary>
    public ObservableCollection<FormaPagamento> FormasPagamento { get; }

    public FormasPagamentoViewModel()
    {
        // Busca as 4 formas de pagamento fixas do serviço mock
        FormasPagamento = new ObservableCollection<FormaPagamento>(MockDataService.ObterFormasPagamento());
    }
}
