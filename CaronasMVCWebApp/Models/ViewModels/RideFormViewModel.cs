using CaronasMVCWebApp.Models.Enums;
using CaronasMVCWebApp.Validation;
using System;
using System.Collections.Generic;

namespace CaronasMVCWebApp.Models.ViewModels
{
    public class RideFormViewModel
    {

        [MotoristaNaoPodeSerPassageiro]
        public Ride Ride { get; set; }
        public ICollection<Destiny> Destinies { get; set; }

        [PeloMenosUmPassageiro]
        public List<CheckBoxListItem> Passengers { get; set; }
        public List<RoundTrip> RoundTrips { get; set; }

        public RideFormViewModel()
        {
            Passengers = new List<CheckBoxListItem>();
            Ride = new Ride() { RoundTrip = RoundTrip.RoundTrip,
                                Date = DateTime.Now};

        }
        public RideFormViewModel(DateTime date)
        {
            Passengers = new List<CheckBoxListItem>();
            
            Ride = new Ride()
            {
                RoundTrip = RoundTrip.RoundTrip,
                Date = date
            };

        }

        public Ride getRide()
        {
            return this.Ride;
        }
    }
}
