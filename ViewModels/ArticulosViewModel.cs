using Articulos.Models;

namespace Articulos.ViewModels;

public class ArticulosViewModel
{
    public List<Articulo> Articulos {get; set; } = new List<Articulo>();

    public string DescripcionFilter { get; set; }
}