using Parcial2.Data;
using Parcial2.Models;
using Microsoft.EntityFrameworkCore;
using Parcial2.ViewModels;

namespace Parcial2.Services;
public class LocalService : ILocalService
{
    private readonly ArticuloContext _context;
    public LocalService(ArticuloContext context)
    {
        _context = context;
    }
    public void Create(Local obj)
    {
        _context.Add(obj);
        _context.SaveChanges();
    }

    public void Delete(Local obj)
    {
        _context.Remove(obj);
        _context.SaveChanges();
    }

    public List<Local> GetAllLocales()
    {
        var query = GetQuery();
        return query.ToList();
    }
    public List<Local> GetAll()
    {
        return _context.Local.Include(l => l.Articulo).ToList();
    }

    public Local? GetById(int id)
    {
        var local = GetQuery()
                    .Include(l => l.Articulo)
                    .FirstOrDefault(m => m.Id == id);

        return local;
    }

    public void Update(Local obj)
    {
        _context.Update(obj);
        _context.SaveChanges();
    }
    private IQueryable<Local> GetQuery()
    {
        return from descripcion in _context.Local select descripcion;
    }
}