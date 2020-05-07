using CaronasMVCWebApp.Models;
using CaronasMVCWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Services
{
    public class MemberService
    {
        private readonly caronas_app_dbContext _context;

        public MemberService(caronas_app_dbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> FindAllAsync()
        {
            return await _context.Member.OrderBy(x => x.Name).ToListAsync();
        }

        internal async Task<Member> FindByIdAsync(int memberId)
        {
            return await _context.Member.FirstOrDefaultAsync(x => x.Id == memberId);
        }

        public double GetMemberBalance(Member member, DateTime period)
        {
            double balance = 0.0;

            //Get rides in context
            //var rides = from obj in _context.Ride select obj;

            //Filter rides by the period parameter
            var rides = _context.Ride.Where(r => r.Date.Year == period.Year).Where(r => r.Date.Month == period.Month);
            //rides = rides.Where(r => r.Date.Year == period.Year).Where(r => r.Date.Month == period.Month);

            //Sum all rides that the member was the Driver
            balance += rides
                       .Where(r => r.Driver.Equals(member))
                       .Select(r => r.Destiny.CostPerPassenger)
                       .Sum();

            //Subtract all rides that the member was a Passenger
            balance -= rides
                       .Where(r => r.Passenger.Equals(member))
                       .Select(r => r.Destiny.CostPerPassenger)
                       .Sum();

            return balance;
        }

        public string GetMemberPaymentObservation(MonthlyReportViewModel reportObject)
        {


            return "Quem vai pagar quem";
        }
    }
}
