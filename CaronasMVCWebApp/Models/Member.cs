using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "O {0} é obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres")]
        public string Name { get; set; }


        [Required(ErrorMessage = "O {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório.")]
        [Phone(ErrorMessage = "Informe um telefone válido")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        public ICollection<Ride> RideDriver { get; set; }
        public ICollection<Ride> RidePassenger { get; set; }
    }
}
