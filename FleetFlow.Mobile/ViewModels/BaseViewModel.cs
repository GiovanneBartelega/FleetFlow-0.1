using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FleetFlow.Mobile.ViewModels;

/// <summary>
/// Classe base de TODOS os ViewModels do aplicativo.
///
/// Ela implementa a interface INotifyPropertyChanged, que é o mecanismo
/// do .NET que avisa a tela (XAML) quando o valor de uma propriedade muda,
/// fazendo a interface atualizar automaticamente.
///
/// Não usamos nenhum pacote externo (como CommunityToolkit.Mvvm):
/// a implementação abaixo é a forma mais simples e explícita possível.
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Evento disparado sempre que uma propriedade muda de valor.
    /// O MAUI "escuta" este evento para redesenhar a tela.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Dispara o aviso de que uma propriedade mudou.
    /// O parâmetro [CallerMemberName] preenche automaticamente o nome
    /// da propriedade que chamou o método — não precisamos digitá-lo.
    /// </summary>
    /// <param name="nomePropriedade">Nome da propriedade que mudou (preenchido sozinho).</param>
    protected void OnPropertyChanged([CallerMemberName] string nomePropriedade = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
    }

    /// <summary>
    /// Troca o valor de um campo e avisa a tela, tudo em um passo só.
    /// Retorna true se o valor realmente mudou.
    ///
    /// Exemplo de uso em uma propriedade:
    ///   private string _nome = "";
    ///   public string Nome
    ///   {
    ///       get => _nome;
    ///       set => SetProperty(ref _nome, value);
    ///   }
    /// </summary>
    protected bool SetProperty<T>(ref T campo, T valor, [CallerMemberName] string nomePropriedade = "")
    {
        // Se o valor novo é igual ao atual, não faz nada (evita redesenho desnecessário)
        if (EqualityComparer<T>.Default.Equals(campo, valor))
            return false;

        campo = valor;
        OnPropertyChanged(nomePropriedade);
        return true;
    }
}
