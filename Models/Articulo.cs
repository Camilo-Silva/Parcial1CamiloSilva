using System.ComponentModel.DataAnnotations;
using Articulos.Utils;

namespace Articulos.Models;

public class Articulo
{
    public int Id { get; set; }
    [Display(Name = "Nombre de Prenda")]
    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }
    public ArticuloCategoria Categoria { get; set; }

    public bool IsPromo { get; set; }

    public int Stock { get; set; }

    // Relacionamos la lista de Locales con el Articulo
    public virtual List<Local> Locales { get; set; }

}