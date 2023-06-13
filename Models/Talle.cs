using System.ComponentModel.DataAnnotations;
using Parcial2.Utils;

namespace Parcial2.Models;

public class Talle
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public string Descripcion { get; set; }

    public virtual List<Articulo> Articulos { get; set; }
}