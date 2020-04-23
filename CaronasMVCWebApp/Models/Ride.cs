using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.Models
{
    public partial class Ride
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DestinyId { get; set; }
        public int DriverId { get; set; }
        public int PassengerId { get; set; }

        public Destiny Destiny { get; set; }
        public Member Driver { get; set; }
        public Member Passenger { get; set; }
    }
}
