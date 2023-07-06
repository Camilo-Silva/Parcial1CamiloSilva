using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial2.Data;
using Parcial2.Models;
using Parcial2.Services;
using Parcial2.ViewModels;

namespace Parcial2.Controllers
{
    public class LocalController : Controller
    {
        //Inyectamos servicio
        private ILocalService _localService;
        private IArticuloService _articuloService;

        //Pasamos por parametro Interface para poder implementar la interface
        public LocalController(
            ILocalService localService,
            IArticuloService articuloService)
        {
            _localService = localService;
            _articuloService = articuloService;
        }

        // GET: Local
        public IActionResult Index()
        {
            var list = _localService.GetAllLocales();
            return View(list);
        }

        // GET: Local/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = _localService.GetById(id.Value);
            if ((local) == null)
            {
                return NotFound();
            }

            var viewModel = new LocalDetailViewModel();
            viewModel.NombreDeSucursal = local.NombreDeSucursal;
            viewModel.Direccion = local.Direccion;
            viewModel.Telefono = local.Telefono;
            viewModel.Mail = local.Mail;
            viewModel.Articulo = local.Articulo; //!= null ? local.Articulo : new List<Articulo>();

            return View(viewModel);
        }

        // GET: Local/Create
        public IActionResult Create()
        {
            var articulosList = _articuloService.GetAll();
            ViewData["Articulo"] = new SelectList(articulosList, "Id", "Articulo");
            return View();
        }

        // POST: Local/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NombreDeSucursal,Direccion,Telefono,Mail,ArticuloId")] Local local)
        {
            ModelState.Remove("Articulo");
            if (ModelState.IsValid)
            {
                // var articulos = _articuloService.GetAll().Where(x=> localService.ArticuloId.Contains(x.Id));
                _localService.Create(local);

                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(_articuloService.GetAll(), "Id", "Id", local.ArticuloId);
            return View(local);
        }

        // GET: Local/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = _localService.GetById(id.Value);
            if (local == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_articuloService.GetAll(), "Id", "Id", local.ArticuloId);
            return View(local);
        }

        // POST: Local/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,NombreDeSucursal,Direccion,Telefono,Mail,ArticuloId")] Local local)
        {
            if (id != local.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Articulo");
            ModelState.Remove("Talles");

            if (ModelState.IsValid)
            {
                _localService.Update(local);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(_articuloService.GetAll(), "Id", "Id", local.ArticuloId);
            return View(local);
        }

        // GET: Local/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = _localService.GetById(id.Value);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // POST: Local/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var local = _localService.GetById(id);
            if (local != null)
            {
                _localService.Delete(local);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LocalExists(int id)
        {
            return _localService.GetById(id) != null;
        }
    }
}
