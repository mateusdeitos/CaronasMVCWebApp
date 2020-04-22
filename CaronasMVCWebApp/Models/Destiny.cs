using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.Models
{
    public partial class Destiny
    {
        public Destiny()
        {
            Ride = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double CostPerPassenger { get; set; }

        public ICollection<Ride> Ride { get; set; }
    }
}
