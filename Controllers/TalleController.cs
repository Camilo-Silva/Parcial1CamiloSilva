using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial2.Data;
using Parcial2.Models;
using Parcial2.ViewModels;

namespace Parcial2.Controllers
{
    public class TalleController : Controller
    {
        private readonly ArticuloContext _context;

        public TalleController(ArticuloContext context)
        {
            _context = context;
        }

        // GET: Talle
        public async Task<IActionResult> Index()
        {
              return _context.Talle != null ? 
                          View(await _context.Talle.ToListAsync()) :
                          Problem("Entity set 'ArticuloContext.Talle'  is null.");
        }

        // GET: Talle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Talle == null)
            {
                return NotFound();
            }

            // var talle = await _context.Talle.FirstOrDefaultAsync(m => m.Id == id);
            var talle = await _context.Talle.Include(x => x.Articulos).FirstOrDefaultAsync(m => m.Id == id);
            if (talle == null)
            {
                return NotFound();
            }

            var viewModel = new TalleDetailViewModel();
            viewModel.Codigo = talle.Codigo;
            viewModel.Descripcion = talle.Descripcion;
            viewModel.Articulos = talle.Articulos != null ? talle.Articulos : new List<Articulo>();

            return View(viewModel);
        }

        // GET: Talle/Create
        public IActionResult Create()
        {
            ViewData["Articulos"] = new SelectList(_context.Articulo.ToList(), "Id", "Descripcion");
            return View();
        }

        // POST: Talle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Descripcion,ArticuloIds")] TalleCreateViewModel talleView)
        {            
            if (ModelState.IsValid)
            {             
                var articulos = _context.Articulo.Where(x=> talleView.ArticuloIds.Contains(x.Id)).ToList();
                var talle = new Talle{
                    Codigo = talleView.Codigo,
                    Descripcion = talleView.Descripcion,
                    Articulos = articulos
                };
                _context.Add(talle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talleView);
        }

        // GET: Talle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Talle == null)
            {
                return NotFound();
            }

            var talle = await _context.Talle.FindAsync(id);
            if (talle == null)
            {
                return NotFound();
            }
            
            var viewModel = new TalleEditViewModel();
            viewModel.Id = talle.Id;
            viewModel.Codigo = talle.Codigo;
            viewModel.Descripcion = talle.Descripcion;                        
            // viewModel.ArticuloIds = talle.Articulos != null ? talle.Articulos : new List<int>();
            return View(viewModel);
        }

        // POST: Talle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Descripcion")] Talle talle)
        {
            if (id != talle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TalleExists(talle.Id))
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
            return View(talle);
        }

        // GET: Talle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Talle == null)
            {
                return NotFound();
            }

            var talle = await _context.Talle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talle == null)
            {
                return NotFound();
            }

            return View(talle);
        }

        // POST: Talle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Talle == null)
            {
                return Problem("Entity set 'ArticuloContext.Talle'  is null.");
            }
            var talle = await _context.Talle.FindAsync(id);
            if (talle != null)
            {
                _context.Talle.Remove(talle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalleExists(int id)
        {
          return (_context.Talle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
