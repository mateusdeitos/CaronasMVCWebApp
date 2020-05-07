using System;
using System.Collections.Generic;
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
        public Dictionary<Member, double> Members { get; set; }
        public List<string> PaymentObservation { get; set; }
    }
}
