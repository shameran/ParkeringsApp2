using System;
using System.Collections.Generic;

namespace ParkeringsApp
{
    public class Parkeringshus
    {
        private const int TotalaPlatser = 25;
        private List<Fordon> fordon = new List<Fordon>(new Fordon[TotalaPlatser]);

        // Parkera fordon på en ledig plats
        public string ParkeraFordon(Fordon fordon, double varaktighet)
        {
            double storlek = fordon.FåStorlek();
            int startIndex = HittaLedigPlats(storlek);

            if (startIndex != -1)
            {
                // Parkera fordonet på den hittade platsen
                for (int i = 0; i < storlek; i++)
                {
                    this.fordon[startIndex + i] = fordon;  // Ta upp flera platser om det behövs
                }

                fordon.Parkeringstid = varaktighet;  // Sätt parkeringstiden
                return $"Parkerat på plats {startIndex + 1}";
            }
            return "Ingen plats tillgänglig";
        }

        // Hitta en ledig plats för ett fordon baserat på dess storlek
        private int HittaLedigPlats(double storlek)
        {
            for (int i = 0; i < TotalaPlatser; i++)
            {
                if (KanPassa(storlek, i))
                {
                    return i;  // Hittat en ledig plats
                }
            }
            return -1;  // Ingen tillgänglig plats
        }

        // Kolla om ett fordon kan passa på en viss plats baserat på storlek
        private bool KanPassa(double storlek, int index)
        {
            if (storlek == 1) // Storleken bilen tar plats = 1 PLATS
            {
                return fordon[index] == null;  // Bil (1 plats)
            }
            else if (storlek == 2) // Storleken den tar. Alltså buss tar 2 platser
            {
                // Buss (2 platser) måste också kolla om den andra platsen är ledig
                return index + 1 < TotalaPlatser && fordon[index] == null && fordon[index + 1] == null;
            }
            else if (storlek == 0.5) // Halv Plats
            {
                return fordon[index] == null;  // Motorcykel (0.5 plats)
            }
            return false;
        }

        // Lista alla parkerade fordon
        public void ListaFordons()
        {
            bool finnsFordon = false;

            for (int i = 0; i < TotalaPlatser; i++)
            {
                if (fordon[i] != null)
                {
                    Fordon v = fordon[i];
                    Console.WriteLine($"Plats {i + 1}: {v.GetType().Name} {v.Registreringsnummer} {v.Färg}");
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
            var fordon = this.fordon.Find(v => v.Registreringsnummer == registreringsnummer);
            if (fordon != null)
            {
                double pris = fordon.BeräknaPris();  // Beräkna priset baserat på parkeringstiden
                // Ta bort fordonet från parkeringen
                for (int i = 0; i < fordon.FåStorlek(); i++)
                {
                    this.fordon[this.fordon.IndexOf(fordon) + i] = null;  // Ta bort fordonet
                }

                Console.WriteLine($"Fordon med registreringsnummer {registreringsnummer} checkades ut.");
                Console.WriteLine($"Parkeringstid: {fordon.Parkeringstid} sekunder.");
                Console.WriteLine($"Totalt att betala: {pris} kr");
            }
            else
            {
                Console.WriteLine("Fordonet hittades inte.");
            }
        }

        // Visar parkeringens status med färgkodade platser
        public void VisaParkering()
        {
            Console.Clear();
            Console.WriteLine("Parkeringshus Status:");

            char rad = 'A'; // Startar först med rad A sen B, C, D, E
            for (int i = 0; i < TotalaPlatser; i++)
            {
                // Om det är en ny rad, skriv ut radbokstaven och platsnummer
                if (i % 5 == 0 && i != 0)
                {
                    rad++;  // Går vidare till nästa rad
                }

                // Kolla om platsen är ledig eller upptagen
                if (fordon[i] == null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;  // Ledig plats
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;  // Upptagen plats
                    Console.Write($"[{rad}{(i % 5) + 1}] ");
                }

                // Var femte plats, gör det ett radbyte
                if ((i + 1) % 5 == 0)
                {
                    Console.WriteLine();
                }
            }

            Console.ResetColor();  // Återställ färg
            Console.WriteLine();
        }
    }
}