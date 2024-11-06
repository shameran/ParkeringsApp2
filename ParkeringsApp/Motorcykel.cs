using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkeringsApp
{
    public class Motorcykel : Fordon
    {
        public string Märke { get; set; }

        public Motorcykel(string registreringsnummer, string färg, string märke)
            : base(registreringsnummer, färg)
        {
            Märke = märke;
        }

        public override double FåStorlek() => 0.5; // Tar 0.5 parkeringsplats
    }
}