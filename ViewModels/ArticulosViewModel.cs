using Parcial2.Models;

namespace Parcial2.ViewModels;

public class ArticulosViewModel
{
    public List<Articulo> Articulos {get; set; } = new List<Articulo>();

    public string DescripcionFilter { get; set; }
}