using FleetFlow.Mobile.ViewModels;

namespace FleetFlow.Mobile.Views;

/// <summary>
/// Code-behind da tela de Categorias financeiras.
/// </summary>
public partial class CategoriasPage : ContentPage
{
    public CategoriasPage()
    {
        InitializeComponent();

        // Conecta a tela ao ViewModel, que carrega as 11 categorias mock
        BindingContext = new CategoriasViewModel();
    }

    /// <summary>
    /// Evento de clique do botão "+" (nova categoria).
    /// O cadastro ainda não existe — exibimos apenas um alerta simulado.
    /// </summary>
    private async void OnNovaCategoriaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Nova categoria", "Cadastro em desenvolvimento", "OK");
    }
}
