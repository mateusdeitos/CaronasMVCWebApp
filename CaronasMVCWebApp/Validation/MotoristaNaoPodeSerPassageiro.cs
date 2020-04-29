using CaronasMVCWebApp.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CaronasMVCWebApp.Validation
{
    public class MotoristaNaoPodeSerPassageiroAttribute: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            RideFormViewModel model = validationContext.ObjectInstance as RideFormViewModel;
            int driverId = model.Ride.DriverId;

            var passageirosSelecionados = model.Passengers.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            
            if (!passageirosSelecionados.Contains(driverId))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"O motorista não pode ser um dos passageiros!");
        }

    }
}
