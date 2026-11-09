using HeroesFluent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HeroesFluent.Data;

public class HeroesDbContext : IdentityDbContext<IdentityUser>
{
    public HeroesDbContext(DbContextOptions<HeroesDbContext> options) : base(options)
    {
    }

    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Heroe> Heroes => Set<Heroe>();
    public DbSet<Mision> Misiones => Set<Mision>();
    public DbSet<HeroeMision> HeroeMisiones => Set<HeroeMision>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HeroesDbContext).Assembly);
    }
}
