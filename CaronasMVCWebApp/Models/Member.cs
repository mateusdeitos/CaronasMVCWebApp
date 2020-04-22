using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.Models
{
    public partial class Member
    {
        public Member()
        {
            Passenger = new HashSet<Passenger>();
            Ride = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Passenger> Passenger { get; set; }
        public ICollection<Ride> Ride { get; set; }
    }
}
