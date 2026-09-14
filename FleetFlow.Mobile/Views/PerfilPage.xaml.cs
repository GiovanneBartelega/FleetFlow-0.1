namespace FleetFlow.Mobile.Views;

/// <summary>
/// Code-behind da tela de Perfil do usuário.
/// </summary>
public partial class PerfilPage : ContentPage
{
    public PerfilPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Evento de clique do botão "Sair da conta".
    /// A navegação é apenas visual: voltamos para a tela de login
    /// usando navegação absoluta ("//" troca a pilha inteira de telas).
    /// </summary>
    private async void OnSairClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//login");
    }
}
