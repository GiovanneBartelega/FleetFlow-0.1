using FleetFlow.Mobile.ViewModels;

namespace FleetFlow.Mobile.Views;

/// <summary>
/// Code-behind da tela de Formas de Pagamento.
/// </summary>
public partial class FormasPagamentoPage : ContentPage
{
    public FormasPagamentoPage()
    {
        InitializeComponent();

        // Conecta a tela ao ViewModel, que carrega as 4 formas de pagamento mock
        BindingContext = new FormasPagamentoViewModel();
    }

    /// <summary>
    /// Evento de clique do botão "+" (nova forma de pagamento).
    /// O cadastro ainda não existe — exibimos apenas um alerta simulado.
    /// </summary>
    private async void OnNovaFormaPagamentoClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Nova forma de pagamento", "Cadastro em desenvolvimento", "OK");
    }
}
