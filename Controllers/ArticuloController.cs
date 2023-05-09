using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Articulos.Data;
using Articulos.Models;
using Articulos.ViewModels;

namespace Articulos.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly ArticuloContext _context;

        public ArticuloController(ArticuloContext context)
        {
            _context = context;
        }

        // GET: Articulo
        public async Task<IActionResult> Index(string DescripcionFilter)
        {
            var query = from descripcion in _context.Articulo select descripcion;

            if (!string.IsNullOrEmpty(DescripcionFilter))
            {
                query = query.Where(x => x.Descripcion.ToUpper().Contains(DescripcionFilter));
            }
            var model = new ArticulosViewModel();
            model.Articulos = await query.ToListAsync();

              return _context.Articulo != null ? 
                          View(model) :
                          Problem("Entity set 'ArticuloContext.Articulo'  is null.");
        }

        // GET: Articulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articulo == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulo.Include(x=> x.Locales).FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            var viewModel = new ArticulosDetailViewModel();
            viewModel.Id = articulo.Id;
            viewModel.Descripcion = articulo.Descripcion;
            viewModel.Categoria = articulo.Categoria.ToString();
            viewModel.Precio = articulo.Precio;
            viewModel.Stock = articulo.Stock;           
            viewModel.IsPromo = articulo.IsPromo;
            viewModel.Locales = articulo.Locales != null ? articulo.Locales : new List<Local>();

            return View(viewModel);
        }

        // GET: Articulo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articulo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Precio,Categoria,IsPromo,Stock")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        // GET: Articulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articulo == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulo.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return View(articulo);
        }

        // POST: Articulo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Precio,Categoria,IsPromo,Stock")] Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(articulo);
        }

        // GET: Articulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articulo == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articulo == null)
            {
                return Problem("Entity set 'ArticuloContext.Articulo'  is null.");
            }
            var articulo = await _context.Articulo.FindAsync(id);
            if (articulo != null)
            {
                _context.Articulo.Remove(articulo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
          return (_context.Articulo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
