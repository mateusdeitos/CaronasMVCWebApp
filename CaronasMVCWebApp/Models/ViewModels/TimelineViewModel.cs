using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models.ViewModels
{
    public class TimelineViewModel
    {
        public TimelineViewModel()
        {
            Passengers = new List<Member>();
        }

        public Ride Ride { get; set; }
        public Member Driver { get; set; }
        public Destiny Destiny { get; set; }
        public ICollection<Member> Passengers { get; set; }
    }
}
