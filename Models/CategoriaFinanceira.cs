public class CategoriaFinanceira
{
	public Guid Id { get; set; }
	public string Nome { get; set; } = null!;
	public string? Descricao { get; set; }
	public string Tipo { get; set; } = null!; // "RECEITA" ou "DESPESA"
	public bool Ativo { get; set; } = true;
}