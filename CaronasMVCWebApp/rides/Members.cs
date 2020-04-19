using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.rides
{
    public partial class Members
    {
        public Members()
        {
            RidesDriverNavigation = new HashSet<Rides>();
            RidesPassengerNavigation = new HashSet<Rides>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }

        public ICollection<Rides> RidesDriverNavigation { get; set; }
        public ICollection<Rides> RidesPassengerNavigation { get; set; }
    }
}
