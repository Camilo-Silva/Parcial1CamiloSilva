using System.ComponentModel.DataAnnotations;
using Parcial2.Utils;
namespace Parcial2.Models;
public class Articulo
{
    public int Id { get; set; }
    [Display(Name = "Nombre de Prenda")]
    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }
    public ArticuloCategoria Categoria { get; set; }
    [Display(Name = "Es Promo")]
    public bool IsPromo { get; set; }

    public int Stock { get; set; }

    // Relacionamos la lista de Locales con el Articulo
    public virtual List<Local> Locales { get; set; }
    public virtual List<Talle> Talles { get; set; }

}