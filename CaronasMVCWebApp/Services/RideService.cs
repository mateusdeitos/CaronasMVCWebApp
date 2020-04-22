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

        internal async Task<Ride> FindByIdAsync(int? rideId)
        {
            return await _context.Ride.FirstOrDefaultAsync(x => x.Id == rideId);
        }


        public async Task InsertAsync(Ride obj, List<int> passengers)
        {
            var ride = new Ride()
            {
                Date = obj.Date,
                DestinyId = obj.DestinyId,
                Destiny = obj.Destiny,
                Driver = obj.Driver,
                DriverId = obj.DriverId,
                Id = obj.Id
            };

            foreach (var passengerID in passengers)
            {
                var passenger = new Passenger { PassengerId = passengerID, RideId = ride.Id};
                ride.Passenger.Add(passenger);
            }
            _context.Add(ride);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, DateTime date, int destinyId, int driverId, List<int> passengers)
        {
            Ride ride = await _context.Ride.Where(x => x.Id == id).FirstAsync();
            ride.Date = date;
            ride.DestinyId = destinyId;
            ride.DriverId = driverId;
            ride.Passenger.Clear();

            foreach (var passengerID in passengers)
            {
                var passenger = new Passenger { PassengerId = passengerID, RideId = ride.Id };
                ride.Passenger.Add(passenger);
            }
            _context.Update(ride);
            await _context.SaveChangesAsync();

        }
    }
}
