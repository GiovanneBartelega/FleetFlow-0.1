namespace FleetFlow.Mobile.Models;

/// <summary>
/// Representa uma categoria financeira do FleetFlow.
/// Cada movimentação (entrada ou saída de dinheiro) pertence a uma categoria.
/// Exemplo: "Fretes" (entrada), "Combustível" (saída).
/// </summary>
public class Categoria
{
    // Identificador único da categoria (vem do banco de dados na versão real)
    public int Id { get; set; }

    // Nome exibido na tela, ex.: "Combustível"
    public string Nome { get; set; } = "";

    // Tipo da movimentação: "ENTRADA" (dinheiro que entra) ou "SAIDA" (dinheiro que sai)
    public string Tipo { get; set; } = "";

    // Indica se a categoria está ativa e pode ser usada em novas movimentações
    public bool Ativo { get; set; } = true;
}
