using CaronasMVCWebApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaronasMVCWebApp.Models
{
    public partial class Ride
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }


        [Display(Name = "Destino")]
        public int DestinyId { get; set; }


        [Display(Name = "Motorista")]
        public int DriverId { get; set; }

        [Display(Name = "Passageiros")]
        public int PassengerId { get; set; }


        [Display(Name = "Status do Pagamento")]
        public PaymentStatus PaymentStatus { get; set; }
        
        [Required(ErrorMessage = "Informe se a viagem foi de ida e volta ou apenas ida")]
        [Display(Name = "Ida e volta ou apenas ida/volta")]
        public RoundTrip RoundTrip { get; set; }

        [Display(Name = "Destino")]
        public Destiny Destiny { get; set; }

        [Display(Name = "Motorista")]
        public Member Driver { get; set; }
        public Member Passenger { get; set; }
    }
}
