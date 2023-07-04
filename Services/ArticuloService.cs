using Microsoft.EntityFrameworkCore;
using Parcial2.Data;
using Parcial2.Models;
using Parcial2.ViewModels;

namespace Parcial2.Services;

public class ArticuloService : IArticuloService
{
    private readonly ArticuloContext _context;

    public ArticuloService(ArticuloContext context)
    {
        _context = context;
    }
    public void Create(Articulo obj)
    {
        _context.Add(obj);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var obj = GetById(id);
        if (obj != null)
        {
            _context.Remove(obj);
        _context.SaveChanges();
        }        
    }

    public List<Articulo> GetAll()
    {
        var query = GetQuery();
        return query.ToList();
    }

    public List<Articulo> GetAll(string filter)
    {
        var query = GetQuery();
        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.Descripcion.ToUpper().Contains(filter));
        }
        return query.ToList();
    }

    public Articulo? GetById(int id)
    {
        var articulo = GetQuery()
                    .Include(x => x.Locales).Include(x => x.Talles)
                    .FirstOrDefault(m => m.Id == id);

        return articulo;
    }

    public void Update(Articulo obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }

    private IQueryable<Articulo> GetQuery()
    {
        return from descripcion in _context.Articulo select descripcion;
    }
}

