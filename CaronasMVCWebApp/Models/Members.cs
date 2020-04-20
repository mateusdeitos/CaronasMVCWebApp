using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaronasMVCWebApp.Models
{
    public partial class Members
    {
        public int Id { get; set; }


        //[Required(ErrorMessage = "{0} é obrigatório.")]
        //[StringLength(60, MinimumLength = 3, ErrorMessage = "O comprimento mínimo do {0} deve ser entre {2} e {1}")]
        public string Nome { get; set; }


        //[Required(ErrorMessage = "{0} é obrigatório.")]
        //[EmailAddress(ErrorMessage = "Informe um endereço de e-mail válido")]
        //[DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Phone(ErrorMessage = "Informe um número de telefone válido")]
        //[DataType(DataType.PhoneNumber)]
        public string Fone { get; set; }
    }
}
