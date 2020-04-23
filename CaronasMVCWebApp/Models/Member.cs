using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.Models
{
    public partial class Member
    {
        public Member()
        {
            RideDriver = new HashSet<Ride>();
            RidePassenger = new HashSet<Ride>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Ride> RideDriver { get; set; }
        public ICollection<Ride> RidePassenger { get; set; }
    }
}
