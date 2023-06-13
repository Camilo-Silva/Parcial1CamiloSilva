using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parcial2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial2.Data;
using Parcial2.Models;
using Parcial2.ViewModels;
using Parcial2.Utils;

namespace Parcial2.Controllers
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
            

            var articulo = await _context.Articulo.Include(x => x.Locales).FirstOrDefaultAsync(m => m.Id == id);
            var articulo2 = await _context.Articulo.Include(x => x.Talles).FirstOrDefaultAsync(m => m.Id == id);
            if ((articulo) == null)
            {
                return NotFound();
            }
            if ((articulo2) == null)
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
            viewModel.Talles = articulo2.Talles != null ? articulo2.Talles : new List<Talle>();

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
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Precio,Categoria,IsPromo,Stock")] ArticulosCreateViewModel articuloView)
        {

            if (ModelState.IsValid)
            {
                var articulo = new Articulo
                {
                    Descripcion = articuloView.Descripcion,
                    Precio = articuloView.Precio,
                    Categoria = (ArticuloCategoria)Enum.Parse(typeof(ArticuloCategoria), articuloView.Categoria),
                    IsPromo = articuloView.IsPromo,
                    Stock = articuloView.Stock
                };
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(articuloView);
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
            ModelState.Remove("Locales");
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
