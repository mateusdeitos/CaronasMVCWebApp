using CaronasMVCWebApp.Models;
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

        public RideService(caronas_app_dbContext context)
        {
            _context = context;
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
                    PassengerId = passengerID
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
    }
}
