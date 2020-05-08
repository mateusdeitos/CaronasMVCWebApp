using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models.ViewModels
{
    public class MonthlyReportViewModel
    {
        public MonthlyReportViewModel()
        {
            Members = new Dictionary<Member, double>();
        }

        public DateTime Period { get; set; }

        [DisplayName("Nome")]
        public Dictionary<Member, double> Members { get; set; }


        [DisplayName("Balanço")]
        public List<string> PaymentObservation { get; set; }
    }
}
