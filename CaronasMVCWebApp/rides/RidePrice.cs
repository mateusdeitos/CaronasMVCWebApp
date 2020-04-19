using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.rides
{
    public partial class RidePrice
    {
        public RidePrice()
        {
            Rides = new HashSet<Rides>();
        }

        public int Id { get; set; }
        public string Destiny { get; set; }
        public double Price { get; set; }

        public ICollection<Rides> Rides { get; set; }
    }
}
