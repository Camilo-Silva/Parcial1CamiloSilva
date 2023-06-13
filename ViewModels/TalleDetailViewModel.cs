using Parcial2.Models;

namespace Parcial2.ViewModels;

public class TalleDetailViewModel
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public string Descripcion { get; set; }

    public virtual List<int> ArticuloIds { get; set; }
    public virtual List<Articulo> Articulos { get; set; }
}
