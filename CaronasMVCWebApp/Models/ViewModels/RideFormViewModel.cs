using CaronasMVCWebApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models.ViewModels
{
    public class RideFormViewModel
    {
        public Ride Ride { get; set; }
        public ICollection<Destiny> Destinies { get; set; }
        public List<CheckBoxListItem> Passengers { get; set; }
        public List<RoundTrip> RoundTrips { get; set; }

        public RideFormViewModel()
        {
            Passengers = new List<CheckBoxListItem>();
            Ride = new Ride() { RoundTrip = RoundTrip.RoundTrip,
                                Date = DateTime.Now};
            
        }
    }
}
