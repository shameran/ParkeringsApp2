using System;
using System.Collections.Generic;

namespace ParkeringsApp
{
    public class Parkeringshus
    {
        private const int TotalaPlatser = 25;
        private List<Fordon>[] fordon = new List<Fordon>[TotalaPlatser];

        public Parkeringshus()
        {
            // Initialisera varje parkeringsplats som en tom lista
            for (int i = 0; i < TotalaPlatser; i++)
            {
                fordon[i] = new List<Fordon>();
            }
        }


        public string ParkeraFordon(Fordon fordonAttParkera, double varaktighet)
        {
            double storlek = fordonAttParkera.FåStorlek();
            int startIndex = HittaLedigPlats(storlek);

            if (startIndex != -1)
            {
                fordonAttParkera.Parkeringstid = varaktighet;  // Sätt parkeringstid

                if (storlek == 0.5)  // Motorcykel (0.5 plats)
                {
                    for (int i = startIndex; i < TotalaPlatser; i++)
                    {
                        if (fordon[i].Count < 2) // Om det finns plats för en till motorcykel
                        {
                            fordon[i].Add(fordonAttParkera);

                            if (fordon[i].Count == 2)
                            {
                                return $"Båda motorcyklarna parkerade på plats {i + 1} ({(char)('A' + i / 5)}{(i % 5) + 1})";
                            }
                            return $"Första motorcykeln parkerad på plats {i + 1} ({(char)('A' + i / 5)}{(i % 5) + 1})";
                        }
                    }
                }
                else if (storlek == 2) // Buss (2 platser)
                {
                    // Försök att hitta två intilliggande lediga platser för bussen på samma rad
                    for (int i = 0; i < TotalaPlatser - 1; i++)  // Vi letar efter ett par platser
                    {
                        // Kontrollera att vi inte korsar radgränser
                        if ((i % 5 != 4) && fordon[i].Count == 0 && fordon[i + 1].Count == 0)
                        {
                            // Parkera bussen på dessa två platser
                            fordon[i].Add(fordonAttParkera);
                            fordon[i + 1].Add(fordonAttParkera);

                            // Vi har nu parkerat bussen på dessa två platser
                            return $"Buss parkerad på plats {i + 1} och {i + 2} ({(char)('A' + i / 5)}{(i % 5) + 1}, {(char)('A' + (i + 1) / 5)}{((i + 1) % 5) + 1})";
                        }
                    }

                    // Om inga lediga intilliggande platser finns på samma rad, försök på nästa rad
                    for (int i = 5; i < TotalaPlatser - 1; i++)  // Börja på index 5 för att söka på andra raden
                    {
                        if ((i % 5 != 4) && fordon[i].Count == 0 && fordon[i + 1].Count == 0)
                        {
                            fordon[i].Add(fordonAttParkera);
                            fordon[i + 1].Add(fordonAttParkera);
                            return $"Buss parkerad på plats {i + 1} och {i + 2} ({(char)('A' + i / 5)}{(i % 5) + 1}, {(char)('A' + (i + 1) / 5)}{((i + 1) % 5) + 1})";
                        }
                    }
                    return "Ingen ledig plats för en buss."; // Om ingen ledig plats för buss hittades
                }
                else // Vanlig parkering för bil
                {
                    fordon[startIndex].Add(fordonAttParkera);
                    return $"Parkerad på plats {startIndex + 1} ({(char)('A' + startIndex / 5)}{(startIndex % 5) + 1})";
                }
            }
            return "Ingen ledig plats";  // Om ingen plats hittas för något fordon
        }

        // Hitta en ledig plats för ett fordon baserat på dess storlek
        private int HittaLedigPlats(double storlek)
        {
            if (storlek == 0.5)  // Motorcykel (0.5 plats)
            {
                for (int i = 0; i < TotalaPlatser; i++)
                {
                    if (fordon[i].Count == 0 || (fordon[i].Count == 1 && fordon[i][0].FåStorlek() == 0.5))
                    {
                        return i; // Hittade en giltig plats för motorcykeln
                    }
                }
            }
            else if (storlek == 2)  // Buss (2 platser)
            {
                for (int i = 0; i < TotalaPlatser - 1; i++)  // Vi letar efter ett par platser
                {
                    // Kontrollera om båda platserna på samma rad är lediga
                    if ((i % 5 != 4) && fordon[i].Count == 0 && fordon[i + 1].Count == 0)
                    {
                        return i;  // Hittade två intilliggande lediga platser
                    }
                }
            }
            else // För bilar (1 plats)
            {
                for (int i = 0; i < TotalaPlatser; i++)
                {
                    if (fordon[i].Count == 0)
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
                return fordon[index].Count == 0;
            }
            else if (storlek == 2) // Buss (2 platser)
            {
                // Buss behöver två  lediga platser
                if (index + 1 < TotalaPlatser && fordon[index].Count == 0 && fordon[index + 1].Count == 0)
                {
                    return true;
                }
                return false;
            }
            else if (storlek == 0.5) 
            {
                // En motorcykel kan passa om platsen inte är helt upptagen
                return fordon[index].Count < 2;
            }
            return false;
        }

        // Lista alla parkerade fordon
        public void ListaFordons()
        {
            bool finnsFordon = false;

            for (int i = 0; i < TotalaPlatser; i++)
            {
                if (fordon[i].Count > 0)
                {
                    foreach (var v in fordon[i])
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
        var fordonAttTaBort = fordon[i].Find(v => v.Registreringsnummer == registreringsnummer);

        if (fordonAttTaBort != null)
        {
            // Kontrollera om det är en buss som upptar två platser
            if (fordonAttTaBort.FåStorlek() == 2)
            {
                // För en buss, ta bort från både den aktuella och nästa plats
                fordon[i].Remove(fordonAttTaBort);  // Ta bort från första platsen
                if (i + 1 < TotalaPlatser) 
                {
                    fordon[i + 1].Remove(fordonAttTaBort);  // Ta bort från andra platsen
                }
                
                Console.WriteLine($"Buss med registreringsnummer {registreringsnummer} checkades ut från plats {i + 1} och {i + 2}.");
            }
            else
            {
                // För vanliga fordon (motorcykel, bil), ta bort från en plats
                fordon[i].Remove(fordonAttTaBort);  // Ta bort fordonet från den aktuella platsen
                Console.WriteLine($"Fordon med registreringsnummer {registreringsnummer} checkades ut från plats {i + 1}.");
            }

            double pris = fordonAttTaBort.BeräknaPris();
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
                if (fordon[i].Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }
                else if (fordon[i].Count == 1 && fordon[i][0].FåStorlek() == 0.5)
                {
                    // En motorcykel: halva gul, halva grön
                    SetParkeringFärg(fordon[i][0]);//Sätter fordons speciella fordons färg.
                    Console.Write($"[{rad}");

                    //Andra halvan
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{(i % 5) + 1}");  
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("] "); // ] är grön
                }
                else if (fordon[i].Count == 2 && fordon[i][0].FåStorlek() == 0.5 && fordon[i][1].FåStorlek() == 0.5)
                {
                    SetParkeringFärg(fordon[i][0]);
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }
                else if (fordon[i].Count == 2)
                {
                    // Buss eller bil parkering, hela platsen upptagen
                    SetParkeringFärg(fordon[i][0]);
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }
                else
                {
                    SetParkeringFärg(fordon[i][0]);
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
                    fordon.ParkeringStatus= "Ledig";
                    break;
            }
            return;
        }
    }
}
