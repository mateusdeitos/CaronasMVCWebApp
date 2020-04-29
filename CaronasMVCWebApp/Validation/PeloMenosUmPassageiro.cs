using CaronasMVCWebApp.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CaronasMVCWebApp.Validation
{
    public class PeloMenosUmPassageiroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            RideFormViewModel ride = validationContext.ObjectInstance as RideFormViewModel;

            int passageirosSelecionados = ride.Passengers.Where(x => x.IsChecked).Select(x=>x.ID).ToList().Count;

            if (passageirosSelecionados > 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Selecione ao menos um passageiro!");
        }

    }
}
