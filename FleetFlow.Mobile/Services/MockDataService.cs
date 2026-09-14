using FleetFlow.Mobile.Models;

namespace FleetFlow.Mobile.Services;

/// <summary>
/// Serviço de dados FALSOS (mock) do FleetFlow.
///
/// Este é um projeto acadêmico SEM integração com API real.
/// Todos os dados exibidos nas telas vêm das listas fixas abaixo,
/// que ficam apenas na memória do aplicativo enquanto ele está aberto.
///
/// A classe é "static" para que possamos acessar os dados de qualquer
/// tela sem precisar criar uma instância com "new".
/// Exemplo de uso: MockDataService.ObterCategorias();
/// </summary>
public static class MockDataService
{
    /// <summary>
    /// Retorna as 11 categorias financeiras reais do cliente JP & PH Transportes.
    /// Apenas "Fretes" é ENTRADA (dinheiro que entra); as demais são SAIDA (gastos).
    /// </summary>
    public static List<Categoria> ObterCategorias()
    {
        return new List<Categoria>
        {
            new Categoria { Id = 1,  Nome = "Fretes",            Tipo = "ENTRADA", Ativo = true },
            new Categoria { Id = 2,  Nome = "Combustível",       Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 3,  Nome = "Pedágio",           Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 4,  Nome = "Salário Motorista", Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 5,  Nome = "Financiamento",     Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 6,  Nome = "Seguro",            Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 7,  Nome = "Impostos e Taxas",  Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 8,  Nome = "Manutenção",        Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 9,  Nome = "Contabilidade",     Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 10, Nome = "Rastreador",        Tipo = "SAIDA",   Ativo = true },
            new Categoria { Id = 11, Nome = "Outros Gastos",     Tipo = "SAIDA",   Ativo = true },
        };
    }

    /// <summary>
    /// Retorna as 4 formas de pagamento usadas pela transportadora.
    /// O campo Icone é a letra exibida dentro do círculo âmbar na tela.
    /// </summary>
    public static List<FormaPagamento> ObterFormasPagamento()
    {
        return new List<FormaPagamento>
        {
            new FormaPagamento { Id = 1, Nome = "PIX",           Icone = "P", Ativo = true },
            new FormaPagamento { Id = 2, Nome = "Boleto",        Icone = "B", Ativo = true },
            new FormaPagamento { Id = 3, Nome = "Cartão Itaú",   Icone = "I", Ativo = true },
            new FormaPagamento { Id = 4, Nome = "Cartão Sicoob", Icone = "S", Ativo = true },
        };
    }

    /// <summary>
    /// Retorna os 4 perfis de usuário do sistema.
    /// Apenas o Administrador possui 1 usuário vinculado (o João Pedro).
    /// </summary>
    public static List<Perfil> ObterPerfis()
    {
        return new List<Perfil>
        {
            new Perfil { Id = 1, Nome = "Administrador",  Descricao = "Acesso total ao sistema",                    QuantidadeUsuarios = 1 },
            new Perfil { Id = 2, Nome = "Gestor de Frota", Descricao = "Veículos, viagens e relatórios logísticos", QuantidadeUsuarios = 0 },
            new Perfil { Id = 3, Nome = "Financeiro",      Descricao = "Fluxo de caixa e movimentações",            QuantidadeUsuarios = 0 },
            new Perfil { Id = 4, Nome = "Motorista",       Descricao = "Apontamento de viagens em campo",           QuantidadeUsuarios = 0 },
        };
    }

    /// <summary>
    /// Retorna o único usuário do sistema (dados fixos, simulando o login).
    /// </summary>
    public static UsuarioMock ObterUsuario()
    {
        return new UsuarioMock();
    }
}
