using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parcial2.Models;

namespace Parcial2.Data
{
    public class ArticuloContext : IdentityDbContext
    {
        public ArticuloContext (DbContextOptions<ArticuloContext> options)
            : base(options)
        {
        }

        public DbSet<Parcial2.Models.Articulo> Articulo { get; set; } = default!;

        public DbSet<Parcial2.Models.Local> Local { get; set; } = default!;

        public DbSet<Parcial2.Models.Talle> Talle { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>()
            .HasMany(p => p.Locales)
            .WithOne(p => p.Articulo)
            .HasForeignKey(p => p.ArticuloId);   

            modelBuilder.Entity<Articulo>()
            .HasMany(p => p.Talles)
            .WithMany(p => p.Articulos)
            .UsingEntity("ArticuloTalle");

            //Llama a la clase madre
            base.OnModelCreating(modelBuilder);
        }
    }
}
