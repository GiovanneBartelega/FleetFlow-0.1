using FleetFlow.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetFlow.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Veiculo> Veiculos => Set<Veiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Veiculo>()
            .HasIndex(x => x.Placa)
            .IsUnique();

        modelBuilder.Entity<Veiculo>()
            .Property(x => x.CapacidadeCarga)
            .HasPrecision(10, 2);
    }
}
