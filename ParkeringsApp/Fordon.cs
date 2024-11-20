public abstract class Fordon
{
    public string Registreringsnummer { get; set; }
    public string Färg { get; set; }
    public double Parkeringstid { get; set; }  // Tid i sekunder
    public List<int> ParkingIndex { get; set; } //Spara undan Index för ParkeringsLista
    public List <string> ParkingDisplay { get; set; } // Sparar den visuella parkeringplatsen t.ex. A1, B1
    public string ParkeringStatus { get; set; } // "NyParkerad" | "Validerad" | "Ogiltig"
    public double ParkeringsKostnad { get; set; } // Samlar totala parkering Kostnad
    public double Böter { get; set; }  // Lägg till en ny egenskap för böter

    public Fordon(string registreringsnummer, string färg)
    {
        Registreringsnummer = registreringsnummer;
        Färg = färg;
        Parkeringstid = 0;  // Initialiserar parkeringstid
        ParkeringStatus = "NyParkerad";
        ParkingDisplay = "##";
        Böter = 0;  // Initialisera böter till 0
        ParkingIndex = new List<int>();
    }

    // Returnerar storleken för fordonet (1 = bil, 2 = buss, 0.5 = motorcykel)
    public abstract double FåStorlek();

    // Pris baserat på parkeringstiden
    public double BeräknaPris()
    {
        return Parkeringstid * 1.5 + Böter;  // Lägg till böter i den totala kostnaden
    }
}
