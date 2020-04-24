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

            int nextId = await FindNextIdAsync();
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

        public async Task UpdateAsync(int id, DateTime date, int destinyId, int driverId, List<int> passengers)
        {
            var rides = await _context.Ride.Where(x => x.Id == id).ToListAsync();
            foreach (var ride in rides)
            {
                _context.Ride.Remove(ride);
                await _context.SaveChangesAsync();
            }

            Ride newRide = new Ride();
            newRide.Date = date;
            newRide.DestinyId = destinyId;
            newRide.DriverId = driverId;

            await InsertAsync(newRide, passengers);
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
            //var allRoundTrip = FindAllRoundTripValues();
            viewModel.Destinies = allDestinies;
            //viewModel.RoundTrips = allRoundTrip;
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

            return viewModel;
        }
    }
}
