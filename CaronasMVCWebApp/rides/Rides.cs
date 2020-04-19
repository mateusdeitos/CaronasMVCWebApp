using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.rides
{
    public partial class Rides
    {
        public DateTime Date { get; set; }
        public int Driver { get; set; }
        public int Passenger { get; set; }
        public int Destiny { get; set; }

        public RidePrice DestinyNavigation { get; set; }
        public Members DriverNavigation { get; set; }
        public Members PassengerNavigation { get; set; }
    }
}
