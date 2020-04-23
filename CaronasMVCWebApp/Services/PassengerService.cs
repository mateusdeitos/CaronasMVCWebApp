using CaronasMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Services
{
    public class PassengerService
    {
        private readonly caronas_app_dbContext _context;

        public PassengerService(caronas_app_dbContext context)
        {
            _context = context;
        }

        //public async Task<List<Passenger>> FindAllAsync()
        //{
        //    return await _context.Include(x => x.Member).OrderBy(x => x.Member.Name).ToListAsync();
        //}

        //internal async Task<Passenger> FindByIdAsync(int passengerId)
        //{
        //    return await _context.Passenger.FirstOrDefaultAsync(x => x.PassengerId == passengerId);
        //}

        //internal async Task<List<Passenger>> FindByRideIdAsync(int? rideId)
        //{
        //    return await _context.Passenger.Where(x => x.RideId == rideId).ToListAsync();
        //}
    }
}
