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

            int nextId = obj.Id == 0 ? await FindNextIdAsync() : obj.Id;
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
                    RoundTrip = obj.RoundTrip
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

        public async Task<List<Ride>> FindByMonthAsync(DateTime? month)
        {
            var rideMonth = month.Value.Month;
            var rideYear = month.Value.Year;

            var rides = await _context.Ride.Where(r => r.Date.Month == rideMonth && r.Date.Year == rideYear).ToListAsync();

            return rides;
        }

        public async Task<List<Ride>> FindRidesByDriverAndPeriod(Member driver, DateTime period)
        {
            return await _context.Ride.Where(r => r.DriverId == driver.Id && 
                                             r.Date.Month == period.Month &&
                                             r.Date.Year == period.Year).ToListAsync();
        }
        public async Task<List<Ride>> FindRidesByPassengerAndPeriod(Member passenger, DateTime period)
        {
            return await _context.Ride.Where(r => r.PassengerId == passenger.Id &&
                                             r.Date.Month == period.Month &&
                                             r.Date.Year == period.Year).ToListAsync();
        }

        public async Task<int> FindNextIdAsync()
        {
            return await _context.Ride.AnyAsync() ? await _context.Ride.MaxAsync(r => r.Id + 1) : 1;
        }

        public async Task<List<Ride>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.Ride select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }



            return await result.ToListAsync();
        }

        public async Task<ICollection<AnalyticalReportViewModel>> GenerateAnalyticalReportForMember(List<Ride> result, Member member)
        {
            ICollection<AnalyticalReportViewModel> viewModel = new List<AnalyticalReportViewModel>();
            List<int> uniqueRidesId = result.Select(r => r.Id).Distinct().ToList();

            foreach (int rideId in uniqueRidesId)
            {
                ICollection<CheckBoxListItem> ridePassengers = new List<CheckBoxListItem>();
                var ride = await FindPassengersByRideId(rideId);
                var passengers = ride.Select(r => r.Passenger).ToList();
                if (passengers.Contains(member) || ride.Select(r => r.DriverId).FirstOrDefault() == member.Id)
                {
                    var rideCost = await FindCostPerPassengerByRideIdAsync(rideId);
                    double rideBalance = 0;

                    if (passengers.Contains(member))
                    {
                        rideBalance = rideCost * (-1);
                    }
                    else
                    {
                        rideBalance = rideCost * passengers.Count;
                    }
                    foreach (var passenger in ride)
                    {
                        Member Passenger = await _memberService.FindByIdAsync(passenger.PassengerId);
                        ridePassengers.Add(new CheckBoxListItem { Display = Passenger.Name,
                        ID = Passenger.Id,
                        IsChecked = false});
                    }

                    AnalyticalReportViewModel rideForReport = new AnalyticalReportViewModel
                    {
                        Ride = ride.FirstOrDefault(),
                        Balance = rideBalance
                    };
                    rideForReport.Ride.PassengerId = passengers.Count;
                    viewModel.Add(rideForReport);
                }

            }
            return viewModel;
        }

        public async Task<double> FindCostPerPassengerByRideIdAsync(int id)
        {
            var ride = await FindByIdAsync(id);
            Destiny destiny = await _destinyService.FindByIdAsync(ride.Select(r => r.DestinyId).FirstOrDefault());
            int trip = (int)ride.Select(r => r.RoundTrip).FirstOrDefault();


            return destiny.CostPerPassenger / trip;
        }

        internal async Task<List<Ride>> FindPassengersByRideId(int? id)
        {
            return await _context.Ride.Where(r => r.Id == id).ToListAsync();
        }


        internal List<RoundTrip> FindAllRoundTripValues()
        {
            return Enum.GetValues(typeof(RoundTrip)).Cast<RoundTrip>().ToList();
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
