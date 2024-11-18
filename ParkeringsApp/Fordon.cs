public abstract class Fordon
{
    public string Registreringsnummer { get; set; }
    public string Färg { get; set; }
    public double Parkeringstid { get; set; }  // Tid i sekunder
    public List<double> ParkingIndex { get; set; } //Spara undan Index för ParkeringsLista
    public string ParkingDisplay { get; set; } // Sparar den visuella parkeringplatsen t'ex A1, B1
    public string ParkeringStatus { get; set; } // "Nyparkerad" | "Validerad" | "Ogiltig" | Kan användas för att sätt färger
    public Fordon(string registreringsnummer, string färg)
    {
        Registreringsnummer = registreringsnummer;
        Färg = färg;
        Parkeringstid = 0;  // Initialiserar parkeringstid
        ParkeringStatus = "NyParkerad";
        ParkingDisplay = "##";



    }

    // Returnerar storleken för fordonet (1 = bil, 2 = buss, 0.5 = motorcykel)
    public abstract double FåStorlek();

    // Pris baserat på parkeringstiden
    public double BeräknaPris()
    {
        return Parkeringstid * 1.5;  // Pris per sekund
    }
  
    
}
