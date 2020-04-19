using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp
{
    public partial class RideDestinies
    {
        public RideDestinies()
        {
            Rides = new HashSet<Rides>();
        }

        public int Id { get; set; }
        public string Destiny { get; set; }
        public double Price { get; set; }

        public ICollection<Rides> Rides { get; set; }
    }
}
