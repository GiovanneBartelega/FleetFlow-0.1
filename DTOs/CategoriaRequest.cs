public class CategoriaRequest
{
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string Tipo { get; set; } = null!; // RECEITA ou DESPESA
}