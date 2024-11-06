using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkeringsApp
{
        public class Parkeringsvakt
        {
            public void KontrolleraFordon(string registreringsnummer, Parkeringshus parkeringshus)
            {
                Console.WriteLine($"Kontrollerar fordon med registreringsnummer: {registreringsnummer}");
            }
        }
    }

