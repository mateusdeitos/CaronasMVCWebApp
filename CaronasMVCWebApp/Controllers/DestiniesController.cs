using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaronasMVCWebApp.Data;
using CaronasMVCWebApp.Models;

namespace CaronasMVCWebApp.Controllers
{
    public class DestiniesController : Controller
    {
        private readonly CaronasMVCWebAppContext _context;

        public DestiniesController(CaronasMVCWebAppContext context)
        {
            _context = context;
        }

        // GET: Destinies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Destinies.ToListAsync());
        }

        // GET: Destinies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinies = await _context.Destinies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destinies == null)
            {
                return NotFound();
            }

            return View(destinies);
        }

        // GET: Destinies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CustoPorPassageiro")] Destinies destinies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destinies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destinies);
        }

        // GET: Destinies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinies = await _context.Destinies.FindAsync(id);
            if (destinies == null)
            {
                return NotFound();
            }
            return View(destinies);
        }

        // POST: Destinies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CustoPorPassageiro")] Destinies destinies)
        {
            if (id != destinies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destinies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestiniesExists(destinies.Id))
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
            return View(destinies);
        }

        // GET: Destinies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinies = await _context.Destinies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (destinies == null)
            {
                return NotFound();
            }

            return View(destinies);
        }

        // POST: Destinies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destinies = await _context.Destinies.FindAsync(id);
            _context.Destinies.Remove(destinies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestiniesExists(int id)
        {
            return _context.Destinies.Any(e => e.Id == id);
        }
    }
}
