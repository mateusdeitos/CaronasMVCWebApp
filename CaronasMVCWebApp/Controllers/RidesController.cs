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
    public class RidesController : Controller
    {
        private readonly CaronasMVCWebAppContext _context;

        public RidesController(CaronasMVCWebAppContext context)
        {
            _context = context;
        }

        // GET: Rides
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rides.ToListAsync());
        }

        // GET: Rides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rides = await _context.Rides
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rides == null)
            {
                return NotFound();
            }

            return View(rides);
        }

        // GET: Rides/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data")] Rides rides)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rides);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rides);
        }

        // GET: Rides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rides = await _context.Rides.FindAsync(id);
            if (rides == null)
            {
                return NotFound();
            }
            return View(rides);
        }

        // POST: Rides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data")] Rides rides)
        {
            if (id != rides.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rides);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RidesExists(rides.Id))
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
            return View(rides);
        }

        // GET: Rides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rides = await _context.Rides
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rides == null)
            {
                return NotFound();
            }

            return View(rides);
        }

        // POST: Rides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rides = await _context.Rides.FindAsync(id);
            _context.Rides.Remove(rides);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RidesExists(int id)
        {
            return _context.Rides.Any(e => e.Id == id);
        }
    }
}
