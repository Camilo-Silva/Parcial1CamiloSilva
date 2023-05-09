using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Articulos.Models;

namespace Articulos.Data
{
    public class ArticuloContext : DbContext
    {
        public ArticuloContext (DbContextOptions<ArticuloContext> options)
            : base(options)
        {
        }

        public DbSet<Articulos.Models.Articulo> Articulo { get; set; } = default!;

        public DbSet<Articulos.Models.Local> Local { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulo>()
            .HasMany(p => p.Locales)
            .WithOne(p => p.Articulo)
            .HasForeignKey(p => p.ArticuloId);            
        }
    }
}
