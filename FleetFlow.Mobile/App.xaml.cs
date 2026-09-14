namespace FleetFlow.Mobile;

/// <summary>
/// Ponto de entrada do aplicativo FleetFlow.
/// </summary>
public partial class App : Application
{
	public App()
	{
		// Carrega o App.xaml, que mescla os dicionários de cores e estilos
		InitializeComponent();
	}

	/// <summary>
	/// Cria a janela principal do app.
	/// A "MainPage" do aplicativo é o AppShell, que contém toda a
	/// navegação (tela de login + barra de abas inferior).
	/// </summary>
	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}