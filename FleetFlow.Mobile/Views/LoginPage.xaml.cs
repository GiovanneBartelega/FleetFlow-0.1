namespace FleetFlow.Mobile.Views;

/// <summary>
/// Code-behind da tela de Login.
/// "Code-behind" é o arquivo C# que fica "atrás" do XAML:
/// aqui ficam os eventos de clique e a navegação.
/// </summary>
public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        // Carrega o visual definido no arquivo LoginPage.xaml
        InitializeComponent();
    }

    /// <summary>
    /// Evento de toque do botão "Entrar com Google".
    /// O login é apenas visual: navegamos direto para a Home.
    /// O "//" no início da rota faz uma navegação ABSOLUTA,
    /// trocando a pilha inteira de telas (o login sai de cena).
    /// </summary>
    private async void OnEntrarComGoogleTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//home");
    }
}
