using ParkeringsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Buss : Fordon
{
    public int AntalPassagerare { get; set; }
    public Buss(string registreringsnummer, string färg, int antalPassagerare)
    : base(registreringsnummer, färg )
    {
        AntalPassagerare = antalPassagerare;
    }

    public override double FåStorlek() => 2; // Tar 2 parkeringsplatser
}