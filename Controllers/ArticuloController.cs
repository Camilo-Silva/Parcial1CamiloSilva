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
using Parcial2.Services;

namespace Parcial2.Controllers
{
    public class ArticuloController : Controller
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        // GET: Articulo
        public async Task<IActionResult> Index(string descripcionFilter)
        {

            var model = new ArticulosViewModel();
            model.Articulos = _articuloService.GetAll(descripcionFilter);

            return View(model);
        }

        // GET: Articulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var articulo = _articuloService.GetById(id.Value);
            if ((articulo) == null)
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
            viewModel.Talles = articulo.Talles != null ? articulo.Talles : new List<Talle>();

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
                _articuloService.Create(articulo);
                return RedirectToAction(nameof(Index));
            }
            return View(articuloView);
        }

        // GET: Articulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = _articuloService.GetById(id.Value);
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
            ModelState.Remove("Talles");
            if (ModelState.IsValid)
            {
                try
                {
                    _articuloService.Update(articulo);
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
            if (id == null)
            {
                return NotFound();
            }

            var articulo = _articuloService.GetById(id.Value);
                
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
            _articuloService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
            return _articuloService.GetById(id) != null;
        }
    }
}
