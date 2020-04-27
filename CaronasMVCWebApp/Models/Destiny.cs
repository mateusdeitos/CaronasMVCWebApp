using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaronasMVCWebApp.Models
{
    public partial class Destiny
    {
        public Destiny()
        {
            Ride = new HashSet<Ride>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [Display(Name = "Destino")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
        public string Name { get; set; }


        [Required(ErrorMessage = "O {0} é obrigatório")]
        [Display(Name = "Custo por Passageiro (ida e volta)")]
        [DataType(DataType.Currency)]
        public double CostPerPassenger { get; set; }

        public ICollection<Ride> Ride { get; set; }
    }
}
