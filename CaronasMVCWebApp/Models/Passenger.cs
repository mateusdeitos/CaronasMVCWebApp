using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.Models
{
    public partial class Passenger
    {
        public int PassengerId { get; set; }
        public int RideId { get; set; }

        public Member Member { get; set; }
        public Ride Ride { get; set; }
    }
}
