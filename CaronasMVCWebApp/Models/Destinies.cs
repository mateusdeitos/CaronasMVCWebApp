using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models
{
    public class Destinies
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O comprimento mínimo do {0} deve ser entre {2} e {1}")]
        public String Nome{ get; set; }


        [Required(ErrorMessage = "{0} é obrigatório.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Custo por passageiro (ida e volta)")]
        public double CustoPorPassageiro { get; set; }
    }
}
