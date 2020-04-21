using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaronasMVCWebApp.Models
{
    public class Rides
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Destinies Destino { get; set; }
        public Members Motorista { get; set; }
        public ICollection<Members> Passageiros { get; set; } = new List<Members>();
        public double Custo { get; set; }
    }
}
