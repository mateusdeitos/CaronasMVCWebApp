using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models.ViewModels
{
    public class AnalyticalReportViewModel
    {
        public AnalyticalReportViewModel()
        {

        }

        public Ride Ride { get; set; }
        public double Balance { get; set; }
    }
}
