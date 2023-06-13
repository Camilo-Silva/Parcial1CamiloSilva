using System.ComponentModel.DataAnnotations;

namespace Parcial2.Models;

public class Local
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
    public virtual Articulo Articulo { get; set; }
    // public virtual List<Articulo> Articulos { get; set; }
    
   
}