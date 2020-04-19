using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp
{
    public partial class Rides
    {
        public DateTime Date { get; set; }
        public int Driver { get; set; }
        public int Passenger { get; set; }
        public int Destiny { get; set; }

        public RideDestinies DestinyNavigation { get; set; }
    }
}
