using Microsoft.EntityFrameworkCore;
using FleetFlow.Api.Models;

namespace FleetFlow.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Perfil> Perfis => Set<Perfil>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();
    public DbSet<CategoriaFinanceira> CategoriasFinanceiras => Set<CategoriaFinanceira>();
    public DbSet<FormaPagamento> FormasPagamento => Set<FormaPagamento>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // Mapeia nomes das tabelas (snake_case) pra as classes (PascalCase)
        mb.Entity<Perfil>().ToTable("perfis");
        mb.Entity<Usuario>().ToTable("usuarios");
        mb.Entity<Veiculo>().ToTable("veiculos");
        mb.Entity<CategoriaFinanceira>().ToTable("categorias_financeiras");
        mb.Entity<FormaPagamento>().ToTable("formas_pagamento");

        // UUIDs gerados pelo Postgres
        mb.Entity<Perfil>().Property(p => p.Id).HasDefaultValueSql("gen_random_uuid()");
        mb.Entity<Usuario>().Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
        mb.Entity<CategoriaFinanceira>().Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");
        mb.Entity<FormaPagamento>().Property(f => f.Id).HasDefaultValueSql("gen_random_uuid()");
        mb.Entity<Veiculo>().Property(v => v.Id).HasDefaultValueSql("gen_random_uuid()");
    }
}