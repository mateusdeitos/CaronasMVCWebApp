using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.Models
{
    public partial class Rides
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Members Driver { get; set; }
        public ICollection<Members> Passengers { get; set; } = new List<Members>();

        public RideDestinies Destiny { get; set; }
    }
}
