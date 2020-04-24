using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "Pago")]
        Paid = 1,

        [Display(Name = "Não pago")]
        NotPaid = 0
    }
}
