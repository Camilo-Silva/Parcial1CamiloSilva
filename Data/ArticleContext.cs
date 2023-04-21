using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUD_Articulos_y_Precios.Models;

namespace CRUD_ARTICULOS_Y_PRECIOS.Data
{
    public class ArticleContext : DbContext
    {
        public ArticleContext (DbContextOptions<ArticleContext> options)
            : base(options)
        {
        }

        public DbSet<CRUD_Articulos_y_Precios.Models.Article> Article { get; set; } = default!;
    }
}
