using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkeringsApp
{
    public class Bil : Fordon
    {
        public bool Elbil { get; set; }

        public Bil(string registreringsnummer, string färg, bool elbil)
            : base(registreringsnummer, färg)
        {
            Elbil = elbil;
        }

        public override double FåStorlek() => 1; // Tar 1 parkeringsplats
    }
}
