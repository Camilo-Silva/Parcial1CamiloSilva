using System.ComponentModel.DataAnnotations;
using Parcial2.Models;

namespace Parcial2.ViewModels;

public class LocalDetailViewModel
{
    public int Id { get; set; }
    [Display(Name="Sucursal")]
    public string NombreDeSucursal { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Mail { get; set; }

    //ArticuloId es el ForeignKye
    public int ArticuloId { get; set; }
     
    // Propiedad virtual para usarse en el entityFramework para traer el artículo del local
    // public virtual Articulo Articulo { get; set; }
    public virtual List<Articulo> Articulos { get; set; }
   
}