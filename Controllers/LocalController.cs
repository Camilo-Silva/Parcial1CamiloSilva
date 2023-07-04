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

        //Pasamos por parametro Interface para poder implementar la interface
        public LocalController(ILocalService localService)
        {
            _localService = localService;
        }

        // GET: Local
        public IActionResult Index()
        {
            var list = _localService.GetAll();
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
            if (local == null)
            {
                return NotFound();
            }

            // var viewModel = new LocalDetailViewModel();
            // viewModel.NombreDeSucursal = local.NombreDeSucursal;
            // viewModel.Direccion = local.Direccion;
            // viewModel.Telefono = local.Telefono;
            // viewModel.Mail = local.Mail;
            // viewModel.Articulos = local.Articulos != null ? local.Articulos : new List<Articulo>();

            return View(local);
        }

        // GET: Local/Create
        public IActionResult Create()
        {
            // var menuList = _localService.GetAll();
            ViewData["Articulo"] = new SelectList(new List<Articulo>(), "Id", "Articulo");
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
                _localService.Create(local);

                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(new List<Articulo>(), "Id", "Id", local.ArticuloId);
            return View(local);
        }

        // GET: Local/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["ArticuloId"] = new SelectList(new List<Articulo>(), "Id", "Id", local.ArticuloId);
            return View(local);
        }

        // POST: Local/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreDeSucursal,Direccion,Telefono,Mail,ArticuloId")] Local local)
        {
            if (id != local.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                _localService.Update(local);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticuloId"] = new SelectList(new List<Articulo>(), "Id", "Id", local.ArticuloId);
            return View(local);
        }

        // GET: Local/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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
