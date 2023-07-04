using Parcial2.Models;

namespace Parcial2.Services;

public interface IArticuloService
{
    void Create(Articulo obj);
    List<Articulo> GetAll(string filter);
    void Update(Articulo obj);
    void Delete(int id);
    Articulo? GetById(int id);
}
