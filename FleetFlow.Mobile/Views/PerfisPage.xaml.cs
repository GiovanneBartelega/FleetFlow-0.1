using FleetFlow.Mobile.ViewModels;

namespace FleetFlow.Mobile.Views;

/// <summary>
/// Code-behind da tela de Perfis de Usuário.
/// </summary>
public partial class PerfisPage : ContentPage
{
    public PerfisPage()
    {
        InitializeComponent();

        // Conecta a tela ao ViewModel, que carrega os 4 perfis mock
        BindingContext = new PerfisViewModel();
    }
}
