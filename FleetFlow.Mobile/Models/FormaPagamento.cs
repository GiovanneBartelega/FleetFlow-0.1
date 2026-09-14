namespace FleetFlow.Mobile.Models;

/// <summary>
/// Representa uma forma de pagamento usada pela transportadora.
/// Exemplo: PIX, Boleto, Cartão Itaú, Cartão Sicoob.
/// </summary>
public class FormaPagamento
{
    // Identificador único da forma de pagamento
    public int Id { get; set; }

    // Nome exibido na tela, ex.: "PIX"
    public string Nome { get; set; } = "";

    // Ícone exibido no círculo âmbar ao lado do nome (uma letra ou emoji)
    public string Icone { get; set; } = "";

    // Indica se a forma de pagamento está ativa e disponível para uso
    public bool Ativo { get; set; } = true;
}
