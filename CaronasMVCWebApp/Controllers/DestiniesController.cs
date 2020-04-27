using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaronasMVCWebApp.Models;

namespace CaronasMVCWebApp.Controllers
{
    public class DestiniesController : Controller
    {
        private readonly caronas_app_dbContext _context;

        public DestiniesController(caronas_app_dbContext context)
        {
            _context = context;
        }

        // GET: Destinies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Destiny.ToListAsync());
        }

        // GET: Destinies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destiny = await _context.Destiny
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destiny == null)
            {
                return NotFound();
            }

            ViewBag.Action = "Details";
            ViewBag.Title = "Detalhes do destino";
            return View("Create", destiny);
        }

        // GET: Destinies/Create
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            ViewBag.Title = "Novo destino";
            Destiny destiny = new Destiny();
            return View(destiny);
        }

        // POST: Destinies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CostPerPassenger")] Destiny destiny)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destiny);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Action = "Create";
            ViewBag.Title = "Novo destino";
            return View(destiny);
        }

        // GET: Destinies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destiny = await _context.Destiny.FindAsync(id);
            if (destiny == null)
            {
                return NotFound();
            }
            ViewBag.Action = "Edit";
            ViewBag.Title = "Editar destino";
            return View("Create", destiny);
        }

        // POST: Destinies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CostPerPassenger")] Destiny destiny)
        {
            if (id != destiny.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destiny);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinyExists(destiny.Id))
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
            ViewBag.Action = "Edit";
            ViewBag.Title = "Editar destino";
            return View("Create", destiny);
        }

        // GET: Destinies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destiny = await _context.Destiny
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destiny == null)
            {
                return NotFound();
            }

            ViewBag.Action = "Delete";
            ViewBag.Title = "Deletar destino";
            return View("Create", destiny);
        }

        // POST: Destinies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destiny = await _context.Destiny.FindAsync(id);
            _context.Destiny.Remove(destiny);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestinyExists(int id)
        {
            return _context.Destiny.Any(e => e.Id == id);
        }
    }
}
