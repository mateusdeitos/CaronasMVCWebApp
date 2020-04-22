using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models
{
    public partial class Ride
    {
        public Ride()
        {
            Passenger = new HashSet<Passenger>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DestinyId { get; set; }
        public int DriverId { get; set; }

        public Destiny Destiny { get; set; }
        public Member Driver { get; set; }
        public ICollection<Passenger> Passenger { get; set; } = new HashSet<Passenger>();
    }
}
