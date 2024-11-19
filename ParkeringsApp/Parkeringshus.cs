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

        // Metod för att lägga till böter på ett fordon
        public void LäggTillBöter(string registreringsnummer, double bötesbelopp)
        {
            foreach (var lista in ParkeringsLista)
            {
                var fordon = lista.Find(f => f.Registreringsnummer == registreringsnummer);
                if (fordon != null)
                {
                    fordon.Böter += bötesbelopp;  
                    Console.WriteLine($"Böter på {fordon.Registreringsnummer} har lagts till: {bötesbelopp} SEK");
                    return;
                }
            }
            Console.WriteLine("Fordonet hittades inte.");
        }

        
        public void ModifieraBöter(string registreringsnummer, double nyttBötesbelopp)
        {
            foreach (var lista in ParkeringsLista)
            {
                var fordon = lista.Find(f => f.Registreringsnummer == registreringsnummer);
                if (fordon != null)
                {
                    fordon.Böter = nyttBötesbelopp; 
                    Console.WriteLine($"Böter för {fordon.Registreringsnummer} har modifierats till: {nyttBötesbelopp} SEK");
                    return;
                }
            }
            Console.WriteLine("Fordonet hittades inte.");
        }

        
        public void TaBortBöter(string registreringsnummer)
        {
            foreach (var lista in ParkeringsLista)
            {
                var fordon = lista.Find(f => f.Registreringsnummer == registreringsnummer);
                if (fordon != null)
                {
                    fordon.Böter = 0;  // Sätt bötesbeloppet till 0
                    Console.WriteLine($"Böter för {fordon.Registreringsnummer} har tagits bort.");
                    return;
                }
            }
            Console.WriteLine("Fordonet hittades inte.");
        }
        // Flytta ett fordon från en plats till en annan
        public bool FlyttaFordon(string registreringsnummer, int nyPlatsIndex)
        {
            // Hitta fordonet på den gamla platsen
            for (int i = 0; i < TotalaPlatser; i++)
            {
                var fordon = ParkeringsLista[i].Find(f => f.Registreringsnummer == registreringsnummer);
                if (fordon != null)
                {
                    
                    ParkeringsLista[i].Remove(fordon);

                    // Lägg till fordonet på den nya platsen
                    if (ParkeringsLista[nyPlatsIndex].Count == 0)  // Kontrollera om den nya platsen är ledig
                    {
                        ParkeringsLista[nyPlatsIndex].Add(fordon);
                        // Uppdatera ParkingDisplay för att visa den nya platsen
                        fordon.ParkingDisplay = IndexTillPlats(nyPlatsIndex);
                        return true; 
                    }
                    else
                    {
                        // Om platsen är upptagen, sätt tillbaka fordonet
                        ParkeringsLista[i].Add(fordon);
                        return false;  
                    }
                }
            }
            return false; 
        }

        
        private string IndexTillPlats(int index)
        {
            char rad = (char)('A' + (index / 5));  // Omvandla index till rad 
            int kolumn = (index % 5) + 1;  // Omvandla index till kolumn 
            return $"{rad}{kolumn}";
        }


       
        public bool TaBortFordon(string registreringsnummer)
        {
            for (int i = 0; i < TotalaPlatser; i++)
            {
                var fordon = ParkeringsLista[i].Find(f => f.Registreringsnummer == registreringsnummer);

                if (fordon != null)
                {
                    // Ta bort fordonet från den aktuella platsen
                    ParkeringsLista[i].Remove(fordon);
                    Console.WriteLine($"Fordon med registreringsnummer {registreringsnummer} har tagits bort från parkeringen.");
                    return true;  // Fordonet togs bort
                }
            }
            Console.WriteLine("Fordonet hittades inte.");
            return false;  // Fordonet hittades inte
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
                        if (ParkeringsLista[i].Count < 2) // Kontrollera om det finns plats för en motorcykel till (upp till 2 per plats)
                        {
                            // Lägg till motorcykeln på den aktuella platsen
                            ParkeringsLista[i].Add(fordonAttParkera);

                            if (ParkeringsLista[i].Count == 2)
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
                    if (startIndex + 1 < TotalaPlatser && ParkeringsLista[startIndex].Count == 0 && ParkeringsLista[startIndex + 1].Count == 0)
                    {
                        // Kontrollera att vi inte korsar radgränser
                        if ((i % 5 != 4) && ParkeringsLista[i].Count == 0 && ParkeringsLista[i + 1].Count == 0)
                        {
                                // Parkera bussen på dessa två platser
                            ParkeringsLista[i].Add(fordonAttParkera);
                            ParkeringsLista[i + 1].Add(fordonAttParkera);

                            // Vi har nu parkerat bussen på dessa två platser
                            return $"Buss parkerad på plats {i + 1} och {i + 2} ({(char)('A' + i / 5)}{(i % 5) + 1}, {(char)('A' + (i + 1) / 5)}{((i + 1) % 5) + 1})";
                        }
                    }

                    // Om inga lediga intilliggande platser finns på samma rad, försök på nästa rad
                    for (int i = 5; i < TotalaPlatser - 1; i++)  // Börja på index 5 för att söka på andra raden
                    {
                        if ((i % 5 != 4) && ParkeringsLista[i].Count == 0 && ParkeringsLista[i + 1].Count == 0)
                        {
                            ParkeringsLista[i].Add(fordonAttParkera);
                            ParkeringsLista[i + 1].Add(fordonAttParkera);
                            return $"Buss parkerad på plats {i + 1} och {i + 2} ({(char)('A' + i / 5)}{(i % 5) + 1}, {(char)('A' + (i + 1) / 5)}{((i + 1) % 5) + 1})";
                        }
                    }
                    return "Ingen ledig plats för en buss."; // Om ingen ledig plats för buss hittades
                }
                else // Vanlig parkering för bil
                {
                    ParkeringsLista[startIndex].Add(fordonAttParkera);
                    return $"Parkerad på plats {startIndex + 1}";
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
                    // Kontrollera om platsen är helt ledig eller redan har en motorcykel
                    // - Den inte är upptagen av en bil (som tar hela platsen)
                    // - Den inte är upptagen av en buss (som tar två platser)
                    if (ParkeringsLista[i].Count == 0 || (ParkeringsLista[i].Count == 1 && ParkeringsLista[i][0].FåStorlek() == 0.5))
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
                    if ((i % 5 != 4) && ParkeringsLista[i].Count == 0 && ParkeringsLista[i + 1].Count == 0)
                    {
                        return i;  // Hittade två intilliggande lediga platser
                    }
                }
            }
            else // För bilar (1 plats)
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
            // Kontrollera om det är en buss som upptar två platser
            if (fordonAttTaBort.FåStorlek() == 2)
            {
                        // För en buss, ta bort från både den aktuella och nästa plats
                ParkeringsLista[i].Remove(fordonAttTaBort);  // Ta bort från första platsen
                if (i + 1 < TotalaPlatser) 
                {
                    ParkeringsLista[i + 1].Remove(fordonAttTaBort);  // Ta bort från andra platsen
                }
                
                Console.WriteLine($"Buss med registreringsnummer {registreringsnummer} checkades ut från plats {i + 1} och {i + 2}.");
            }
            else
            {
                // För vanliga fordon (motorcykel, bil), ta bort från en plats
                ParkeringsLista[i].Remove(fordonAttTaBort);  // Ta bort fordonet från den aktuella platsen
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
