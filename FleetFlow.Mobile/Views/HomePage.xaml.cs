using FleetFlow.Mobile.ViewModels;

namespace FleetFlow.Mobile.Views;

/// <summary>
/// Code-behind da tela inicial (Home).
/// Conecta a tela ao HomeViewModel através do BindingContext.
/// </summary>
public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();

        // BindingContext é a "ponte" entre a tela (XAML) e o ViewModel.
        // Todos os {Binding ...} do XAML buscam as propriedades deste objeto.
        BindingContext = new HomeViewModel();
    }

    /// <summary>
    /// Evento de clique do botão "Ver todas as movimentações".
    /// Como não há API real, exibimos apenas um alerta simulado.
    /// DisplayAlert(título, mensagem, texto do botão) mostra um popup nativo.
    /// </summary>
    private async void OnVerMovimentacoesClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Em desenvolvimento",
                           "Esta funcionalidade será implementada em breve.",
                           "OK");
    }
}
