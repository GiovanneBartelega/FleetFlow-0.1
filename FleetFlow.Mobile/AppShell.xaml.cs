using FleetFlow.Mobile.Views;

namespace FleetFlow.Mobile;

/// <summary>
/// Code-behind do Shell: aqui registramos as rotas de navegação
/// usadas pelos comandos GoToAsync("//login") e GoToAsync("//home").
/// </summary>
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Registro das rotas nomeadas do aplicativo.
		// O nome da rota é o "endereço" usado na navegação entre telas.
		Routing.RegisterRoute("login", typeof(LoginPage));
		Routing.RegisterRoute("home", typeof(HomePage));
	}
}
