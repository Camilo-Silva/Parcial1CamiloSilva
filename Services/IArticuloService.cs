using Parcial2.Models;

namespace Parcial2.Services;

public interface IArticuloService
{
    void Create(Articulo obj);
    List<Articulo> GetAll(string descripcionFilter);
    void Update(Articulo obj);
    void Delete(Articulo id);
    Articulo? GetById(int id);
}
