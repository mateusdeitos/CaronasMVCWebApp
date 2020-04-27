using CaronasMVCWebApp.Models;
using CaronasMVCWebApp.Models.Enums;
using CaronasMVCWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Services
{
    public class RideService
    {
        private readonly caronas_app_dbContext _context;
        private readonly MemberService _memberService;
        private readonly DestinyService _destinyService;

        public RideService(caronas_app_dbContext context,
                               MemberService memberService,
                               DestinyService destinyService)
        {
            _context = context;
            _memberService = memberService;
            _destinyService = destinyService;
        }
        public async Task<List<Ride>> FindAllAsync()
        {
            return await _context.Ride.OrderBy(x => x.Date).ToListAsync();
        }

        internal async Task<List<Ride>> FindByIdAsync(int? rideId)
        {
            return await _context.Ride.Where(x => x.Id == rideId).ToListAsync();
        }


        public async Task InsertAsync(Ride obj, List<int> passengers)
        {

            int nextId = obj.Id == 0  ? await FindNextIdAsync() : obj.Id;
            foreach (var passengerID in passengers)
            {
                var ride = new Ride
                {
                    Id = nextId,
                    Date = obj.Date,
                    DestinyId = obj.DestinyId,
                    DriverId = obj.DriverId,
                    PassengerId = passengerID,
                    PaymentStatus = PaymentStatus.NotPaid,
                    RoundTrip = RoundTrip.RoundTrip
                };
                _context.Add(ride);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Ride ride, List<int> passengers)
        {
            var rides = await _context.Ride.Where(x => x.Id == ride.Id).ToListAsync();
            foreach (var r in rides)
            {
                _context.Ride.Remove(r);
                //await _context.SaveChangesAsync();
            }

            await InsertAsync(ride, passengers);
            await _context.SaveChangesAsync();


        }

        public async Task<int> FindNextIdAsync()
        {
            return await _context.Ride.AnyAsync() ? await _context.Ride.MaxAsync(r => r.Id + 1) : 1 ;
        }

        internal async Task<List<Ride>> FindPassengersByRideId(int? id)
        {
            return await _context.Ride.Where(r => r.Id == id).ToListAsync();
        }


        internal List<RoundTrip> FindAllRoundTripValues()
        {
            return  Enum.GetValues(typeof(RoundTrip)).Cast<RoundTrip>().ToList();
        }

        public async Task<RideFormViewModel> StartRideViewModel(RideFormViewModel viewModel)
        {

            var allMembers = await _memberService.FindAllAsync();
            var allDestinies = await _destinyService.FindAllAsync();
            viewModel.Destinies = allDestinies;
            var checkBoxListItems = new List<CheckBoxListItem>();
            var passengers = await FindPassengersByRideId(viewModel.Ride.Id);
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

            return viewModel;
        }
    }
}
