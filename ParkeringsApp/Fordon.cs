public abstract class Fordon
{
    public string Registreringsnummer { get; set; }
    public string Färg { get; set; }
    public double Parkeringstid { get; set; }  // Tid i sekunder

    public Fordon(string registreringsnummer, string färg)
    {
        Registreringsnummer = registreringsnummer;
        Färg = färg;
        Parkeringstid = 0;  //  parkeringstid
    }

    // Returnerar storleken för fordonet (1 = bil, 2 = buss, 0.5 = motorcykel)
    public abstract double FåStorlek();

    // pris baserat på parkeringstiden
    public double BeräknaPris()
    {
        return Parkeringstid * 1.5;  // Pris per sekund
    }
}
