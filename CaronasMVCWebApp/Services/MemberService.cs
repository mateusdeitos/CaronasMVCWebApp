using CaronasMVCWebApp.Models;
using CaronasMVCWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Services
{
    public class MemberService
    {
        private readonly caronas_dbContext _context;

        public MemberService(caronas_dbContext context)
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
                       .Select(r => r.Destiny.CostPerPassenger / (int)r.RoundTrip)
                       .Sum();

            //Subtract all rides that the member was a Passenger
            balance -= rides
                       .Where(r => r.Passenger.Equals(member))
                       .Select(r => r.Destiny.CostPerPassenger / (int)r.RoundTrip)
                       .Sum();

            return balance;
        }

        public List<string> GetMemberPaymentObservation(MonthlyReportViewModel reportObject)
        {
            List<string> observations = new List<string>();

            // Members is a Dictionary<Member,double>
            var debtors = reportObject.Members
                          .Where(m => m.Value < 0)
                          .ToDictionary(m => m.Key, m => m.Value);

            var creditors = reportObject.Members
                           .Where(m => m.Value > 0)
                           .ToDictionary(m => m.Key, m => m.Value);

            // .ToList is necessary because the dictionary values will change inside the foreach
            // If the values change and the Collection used to iterate through Members is the dictionary itself
            // it will throw an exception that the value changed inside the iteration.
            // By using ToList, it will loop inside a list of Members and than be able to change the values.
            foreach (Member debtor in debtors.Keys.ToList())
            {
                foreach (Member creditor in creditors.Keys.ToList())
                {
                    // Get balance of debtor
                    double debtorBalance = Math.Abs(debtors[debtor]);

                    // Get balance of creditor
                    double creditorBalance = Math.Abs(creditors[creditor]);

                    if (creditorBalance > 0 && debtorBalance > 0)
                    {
                        // Calculate the debtor's amount to pay the creditor
                        var amountToPay = debtorBalance >= creditorBalance ? creditorBalance : debtorBalance;
                        observations.Add($"{debtor.Name} deve pagar " +
                                         $"{amountToPay.ToString("F2", CultureInfo.CurrentCulture)} para " +
                                         $"{creditor.Name}");

                        //Update Dictionary values
                        debtors[debtor] += amountToPay;
                        creditors[creditor] -= amountToPay;
                    }
                }
            }
            return observations;
        }
    }
}
