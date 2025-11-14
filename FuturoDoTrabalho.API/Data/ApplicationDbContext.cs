using Microsoft.EntityFrameworkCore;
using FuturoDoTrabalho.API.Models;

namespace FuturoDoTrabalho.API.Data;

/// <summary>
/// Contexto do Entity Framework para gerenciar o banco de dados SQL Server.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Vaga> Vagas { get; set; }
    public DbSet<Candidato> Candidatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da relação entre Vaga e Candidato
        modelBuilder.Entity<Candidato>()
            .HasOne(c => c.Vaga)
            .WithMany(v => v.Candidatos)
            .HasForeignKey(c => c.VagaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Garantir que Email seja único
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}

