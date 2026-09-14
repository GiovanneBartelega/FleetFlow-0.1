using System.Collections.ObjectModel;
using FleetFlow.Mobile.Models;
using FleetFlow.Mobile.Services;

namespace FleetFlow.Mobile.ViewModels;

/// <summary>
/// ViewModel da tela de Categorias financeiras.
/// Carrega a lista de categorias do serviço mock e a expõe para a tela.
///
/// Usamos ObservableCollection porque ela avisa a tela automaticamente
/// quando itens são adicionados ou removidos da lista.
/// </summary>
public class CategoriasViewModel : BaseViewModel
{
    /// <summary>
    /// Lista de categorias exibida na CollectionView da tela.
    /// </summary>
    public ObservableCollection<Categoria> Categorias { get; }

    public CategoriasViewModel()
    {
        // Busca as 11 categorias fixas do serviço mock
        // e coloca dentro de uma ObservableCollection (que a tela entende)
        Categorias = new ObservableCollection<Categoria>(MockDataService.ObterCategorias());
    }
}
