public abstract class Fordon
{
    public string Registreringsnummer { get; set; }
    public string Färg { get; set; }
    public double Parkeringstid { get; set; }  // Tid i sekunder
    public bool SenastKund { get; set; } 
    public int ParkeringRad { get; set; } // X axel i parkeringen
    public char ParkeringKolumn { get; set; } // Y axel i parkeringen
    public string ParkeringStatus { get; set; } // "Nyparkerad" | "Validerad" | "Ogiltig" | Kan användas för att sätt färger
    public Fordon(string registreringsnummer, string färg)
    {
        Registreringsnummer = registreringsnummer;
        Färg = färg;
        Parkeringstid = 0;  // Initialiserar parkeringstid
        ParkeringStatus = "NyParkerad";
        SetParkeringPlats(0 , '#');
        SenastKund = true;

    }

    // Returnerar storleken för fordonet (1 = bil, 2 = buss, 0.5 = motorcykel)
    public abstract double FåStorlek();

    // Pris baserat på parkeringstiden
    public double BeräknaPris()
    {
        return Parkeringstid * 1.5;  // Pris per sekund
    }
    public void SetParkeringPlats(int parkeringRad, char parkeringKolumn)
    {
        ParkeringRad = parkeringRad;
        ParkeringKolumn = parkeringKolumn;
        return;
    }
    
}
