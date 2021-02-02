using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserve_Covid.Models
{
    public class Pais
    {
        public string Country { get; set; }
        public int Posicao { get; set; }
        public int TotalConfirmed{ get; set; }
        public int TotalRecovered { get; set; }
        public int TotalAtived { get; set; }
        public int TotalAtivos { get { return TotalConfirmed - TotalRecovered; } }
    }
}
