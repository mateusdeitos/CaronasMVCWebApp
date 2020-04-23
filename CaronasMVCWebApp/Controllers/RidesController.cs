using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CaronasMVCWebApp.Models;
using CaronasMVCWebApp.Services;
using System.Security.Cryptography;
using CaronasMVCWebApp.Models.ViewModels;
using System.Net.WebSockets;

namespace CaronasMVCWebApp.Controllers
{
    public class RidesController : Controller
    {
        private readonly caronas_app_dbContext _context;
        private readonly MemberService _memberService;
        private readonly DestinyService _destinyService;
        private readonly RideService _rideService;

        public RidesController(caronas_app_dbContext context,
                               MemberService memberService,
                               DestinyService destinyService,
                               RideService rideService)
        {
            _context = context;
            _memberService = memberService;
            _destinyService = destinyService;
            _rideService = rideService;
        }

        // GET: Rides
        public async Task<IActionResult> Index()
        {
            var caronas_app_dbContext = await _context.Ride.Include(r => r.Destiny).Include(r => r.Driver).ToListAsync();
            var results = caronas_app_dbContext.Select(r => r.Id).Distinct().ToList();

            List<Ride> rides = new List<Ride>();
            foreach (int id in results)
            {
                var ride = await _context.Ride.Where(r => r.Id == id).FirstOrDefaultAsync();
                ride.Destiny = await _destinyService.FindByIdAsync(ride.DestinyId);
                ride.Driver = await _memberService.FindByIdAsync(ride.DriverId);
                rides.Add(ride);
                

            }


            return View(rides);
        }

        // GET: Rides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ride = await _context.Ride
                .Include(r => r.Destiny)
                .Include(r => r.Driver)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ride == null)
            {
                return NotFound();
            }

            return View(ride);
        }

        // GET: Rides/Create
        public async Task<IActionResult> Create()
        {
            RideFormViewModel viewModel = new RideFormViewModel();
            var allMembers = await _memberService.FindAllAsync();
            var allDestinies = await _destinyService.FindAllAsync();
            viewModel.Destinies = allDestinies;
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var member in allMembers)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = member.Id,
                    Display = member.Name,
                    IsChecked = false
                });
            }
            viewModel.Passengers = checkBoxListItems;
            return View(viewModel);
        }

        // POST: Rides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RideFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var Passengers = viewModel.Passengers.Where(x => x.IsChecked).Select(x => x.ID).ToList();
                await _rideService.InsertAsync(viewModel.Ride, Passengers);
                return RedirectToAction(nameof(Index));
            }
            var allMembers = await _memberService.FindAllAsync();
            var allDestinies = await _destinyService.FindAllAsync();
            viewModel.Destinies = allDestinies;
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var member in allMembers)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = member.Id,
                    Display = member.Name,
                    IsChecked = false
                });
            }
            viewModel.Passengers = checkBoxListItems;
            return View(viewModel);
        }

        // GET: Rides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            Ride ride = await _context.Ride.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (ride == null)
            {
                return NotFound();
            }

            RideFormViewModel viewModel = new RideFormViewModel()
            {
                Ride = await _context.Ride.Where(r => r.Id == id).FirstOrDefaultAsync()
            };
            var passengers = await _rideService.FindPassengersByRideId(id);
            var allMembers = await _memberService.FindAllAsync();
            var allDestinies = await _destinyService.FindAllAsync();
            viewModel.Destinies = allDestinies;
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var member in allMembers)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = member.Id,
                    Display = member.Name,
                    IsChecked = passengers.Where(x => x.PassengerId == member.Id).Any()
                });
            }
            viewModel.Passengers = checkBoxListItems;

            return View(viewModel);
        }

        // POST: Rides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RideFormViewModel viewModel)
        {
            if (id != viewModel.Ride.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var Passengers = viewModel.Passengers.Where(x => x.IsChecked).Select(x => x.ID).ToList();
                    await _rideService.UpdateAsync(viewModel.Ride.Id, viewModel.Ride.Date, viewModel.Ride.DestinyId, viewModel.Ride.DriverId, Passengers);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RideExists(viewModel.Ride.Id))
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
            var passengers = await _rideService.FindPassengersByRideId(id);
            var allMembers = await _memberService.FindAllAsync();
            var allDestinies = await _destinyService.FindAllAsync();
            viewModel.Destinies = allDestinies;
            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var member in allMembers)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = member.Id,
                    Display = member.Name,
                    IsChecked = passengers.Where(x => x.PassengerId == member.Id).Any()
                });
            }
            viewModel.Passengers = checkBoxListItems;
            return View(viewModel);
        }

        // GET: Rides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ride = await _context.Ride.FirstOrDefaultAsync(r => r.Id == id);
            if (ride == null)
            {
                return NotFound();
            }

            return View(ride);
        }

        // POST: Rides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rides = await _context.Ride.Where(r => r.Id == id).ToListAsync();
            foreach (var ride in rides)
            {

                _context.Ride.Remove(ride);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RideExists(int id)
        {
            return _context.Ride.Any(e => e.Id == id);
        }
    }
}
