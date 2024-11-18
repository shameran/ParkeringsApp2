using System;
using System.Collections.Generic;

namespace ParkeringsApp
{
    public class Parkeringshus
    {
        private const int TotalaPlatser = 25;
        private List<Fordon>[] ParkeringsLista = new List<Fordon>[TotalaPlatser];

        public Parkeringshus()
        {
            // Initialisera varje parkeringsplats som en tom lista
            for (int i = 0; i < TotalaPlatser; i++)
            {
                ParkeringsLista[i] = new List<Fordon>();
            }
        }

        // Parkera ett fordon på en ledig plats
        public string ParkeraFordon(Fordon fordonAttParkera, double varaktighet)
        {
            double storlek = fordonAttParkera.FåStorlek();
            int startIndex = HittaLedigPlats(storlek);

            if (startIndex != -1)
            {
                fordonAttParkera.Parkeringstid = varaktighet;  // Sätt parkeringstid

                if (storlek == 0.5)  // Motorcykel (0.5 plats)
                {
                    // Försök att parkera den första motorcykeln eller den andra på samma plats
                    for (int i = startIndex; i < TotalaPlatser; i++)
                    {
                        if (ParkeringsLista[i].Count < 2) // Kontrollera om det finns plats för en motorcykel till (upp till 2 per plats)
                        {
                            // Lägg till motorcykeln på den aktuella platsen
                            ParkeringsLista[i].Add(fordonAttParkera);

                            if (ParkeringsLista[i].Count == 2)
                            {
                                return $"Båda motorcyklarna parkerade på plats {i + 1}";
                            }
                            return $"Första motorcykeln parkerad på plats {i + 1}";
                        }
                    }
                }
                else if (storlek == 2) // Buss (2 platser)
                {
                    if (startIndex + 1 < TotalaPlatser && ParkeringsLista[startIndex].Count == 0 && ParkeringsLista[startIndex + 1].Count == 0)
                    {
                        ParkeringsLista[startIndex].Add(fordonAttParkera);
                        ParkeringsLista[startIndex + 1].Add(fordonAttParkera);
                        return $"Buss parkerad på plats {startIndex + 1} och {startIndex + 2}";
                    }
                }
                else // Vanlig parkering för bil
                {
                    ParkeringsLista[startIndex].Add(fordonAttParkera);
                    return $"Parkerad på plats {startIndex + 1}";
                }
            }
            return "Ingen ledig plats";
        }

        // Hitta en ledig plats för ett fordon baserat på dess storlek
        private int HittaLedigPlats(double storlek)
        {
            if (storlek == 0.5) 
            {
                for (int i = 0; i < TotalaPlatser; i++)
                {
                    // Kontrollera om platsen är helt ledig eller redan har en motorcykel
                    // - Den inte är upptagen av en bil (som tar hela platsen)
                    // - Den inte är upptagen av en buss (som tar två platser)
                    if (ParkeringsLista[i].Count == 0 || (ParkeringsLista[i].Count == 1 && ParkeringsLista[i][0].FåStorlek() == 0.5))
                    {
                        return i; // Hittade en giltig plats för motorcykeln
                    }
                }
            }
            else 
            {
                for (int i = 0; i < TotalaPlatser; i++)
                {
                    // En plats måste vara helt ledig för en bil eller buss
                    if (ParkeringsLista[i].Count == 0)
                    {
                        return i;
                    }
                }
            }
            return -1;  // Ingen ledig plats
        }

        
        private bool KanPassa(double storlek, int index)
        {
            if (storlek == 1) // Bil (1 plats)
            {
                return ParkeringsLista[index].Count == 0;
            }
            else if (storlek == 2) // Buss (2 platser)
            {
                // Buss behöver två  lediga platser
                if (index + 1 < TotalaPlatser && ParkeringsLista[index].Count == 0 && ParkeringsLista[index + 1].Count == 0)
                {
                    return true;
                }
                return false;
            }
            else if (storlek == 0.5) 
            {
                // En motorcykel kan passa om platsen inte är helt upptagen
                return ParkeringsLista[index].Count < 2;
            }
            return false;
        }

        // Lista alla parkerade fordon
        public void ListaFordons()
        {
            bool finnsFordon = false;

            for (int i = 0; i < TotalaPlatser; i++)
            {
                if (ParkeringsLista[i].Count > 0)
                {
                    foreach (var v in ParkeringsLista[i])
                    {
                        Console.WriteLine($"Plats {i + 1}: {v.GetType().Name} {v.Registreringsnummer} {v.Färg}");
                    }
                    finnsFordon = true;
                }
            }

            if (!finnsFordon)
            {
                Console.WriteLine("Inga fordon parkerade.");
            }
        }

        // Checka ut ett fordon baserat på registreringsnummer och visa pris och parkeringstid
        public void CheckaUtFordon(string registreringsnummer)
        {
            for (int i = 0; i < TotalaPlatser; i++)
            {
                var fordonAttTaBort = ParkeringsLista[i].Find(v => v.Registreringsnummer == registreringsnummer);
                if (fordonAttTaBort != null)
                {
                    double pris = fordonAttTaBort.BeräknaPris();
                    ParkeringsLista[i].Remove(fordonAttTaBort);  // Ta bort fordonet

                    Console.WriteLine($"Fordon med registreringsnummer {registreringsnummer} checkades ut.");
                    Console.WriteLine($"Parkeringstid: {fordonAttTaBort.Parkeringstid} sekunder.");
                    Console.WriteLine($"Totalt att betala: {pris} SEK");
                    return;
                }
            }
            Console.WriteLine("Fordonet hittades inte.");
        }

        // Visa parkeringshusets status med färgkodade platser
        public void VisaParkering()
        {
            Console.Clear();
            Console.WriteLine("Parkeringshus Status:");

            char rad = 'A'; // Starta med rad A, sen B, C, D, E
            for (int i = 0; i < TotalaPlatser; i++)
            {
                // Om det är en ny rad, skriv ut radens bokstav och platsnummer
                if (i % 5 == 0 && i != 0)
                {
                    rad++;  // Gå vidare till nästa rad
                }

                // Kontrollera antalet fordon på platsen
                if (ParkeringsLista[i].Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }
                else if (ParkeringsLista[i].Count == 1 && ParkeringsLista[i][0].FåStorlek() == 0.5)
                {
                    // En motorcykel: halva gul, halva grön
                    SetParkeringFärg(ParkeringsLista[i][0]);//Sätter fordons speciella fordons färg.
                    Console.Write($"[{rad}");

                    //Andra halvan
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{(i % 5) + 1}");  
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("] "); // ] är grön
                }
                else if (ParkeringsLista[i].Count == 2 && ParkeringsLista[i][0].FåStorlek() == 0.5 && ParkeringsLista[i][1].FåStorlek() == 0.5)
                {
                    SetParkeringFärg(ParkeringsLista[i][0]);
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }
                else if (ParkeringsLista[i].Count == 2)
                {
                    // Buss eller bil parkering, hela platsen upptagen
                    SetParkeringFärg(ParkeringsLista[i][0]);
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }
                else
                {
                    SetParkeringFärg(ParkeringsLista[i][0]);
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }

                // Efter var femte plats, gör ett radbyte
                if ((i + 1) % 5 == 0)
                {
                    Console.WriteLine();
                }
            }

            Console.ResetColor();  // Återställ färg
            Console.WriteLine();
        }


        //Ställer in färg baserat på fordonets färg
        public void SetParkeringFärg(Fordon fordon)
        {

            switch (fordon.ParkeringStatus)
            {
                case "NyParkerad":
                    fordon.ParkeringStatus = "Validerad";
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case "Validerad":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case "Ogiltig":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;


                default:
                    Console.ForegroundColor= ConsoleColor.Green;
                    break;
            }
        }
        //Function som ändrar status på ett fordon.
        public void ConfigureParkeringStatus(Fordon fordon, string newStatus) 
        {
            switch (newStatus)
            {
                case "0" or "NyParkerad":
                    fordon.ParkeringStatus = "NyParkerad";
                    break;
                case "1" or "Validerad":
                    fordon.ParkeringStatus = "Validerad";
                    break;
                case "2" or "Ogiltig":
                    fordon.ParkeringStatus = "Ogiltig";
                    break;
                default:
                    return;
            }
            return;
        }
    }
}
