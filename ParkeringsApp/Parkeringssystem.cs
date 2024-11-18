using System;
using System.Collections.Generic;

namespace ParkeringsApp
{
    public class Parkeringssystem
    {
        private Parkeringshus parkeringshus = new Parkeringshus();

        public void Kör()
        {
            while (true)
            {
                Console.Clear();  // Töm skärmen varje gång huvudmenyn visas
                Console.WriteLine("Välj meny:");
                Console.WriteLine("1. Kundmeny");
                Console.WriteLine("2. Adminmeny");
                Console.WriteLine("3. Avsluta");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        KundMeny();
                        break;
                    case "2":
                        AdminMeny();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        private void KundMeny()
        {
            while (true)
            {
                Console.Clear();  // Rensa skärmen när kundmenyn visas
                parkeringshus.VisaParkering();
                Console.WriteLine("Kundmeny:");
                Console.WriteLine("1. Check in");
                Console.WriteLine("2. Check out");
                Console.WriteLine("3. Tillbaka till huvudmenyn");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        ParkeraFordon();
                        break;
                    case "2":
                        CheckaUtFordon();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;

                }
                
            }
        }

        private void AdminMeny()
        {
            while (true)
            {
                Console.Clear();  // Rensa skärmen när adminmenyn visas
                Console.WriteLine("Adminmeny:");
                Console.WriteLine("1. Se parkering");
                Console.WriteLine("2. Se/redigera parkeringstid");
                Console.WriteLine("3. Se fordonsinformation");
                Console.WriteLine("4. Modifiera parkeringsplats");
                Console.WriteLine("5. Hantera tilläggsavgifter");
                Console.WriteLine("6. Tillbaka till huvudmenyn");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        // Visa parkering med platsstatus
                        parkeringshus.VisaParkering();
                        break;
                    case "2":
                        // Kod för att se/redigera parkeringstid (är inte implementerat än)
                        break;
                    case "3":
                        // Visa fordonsinformation för alla parkerade fordon
                        parkeringshus.ListaFordons();  // Visa alla parkerade fordon
                        break;
                    case "4":
                        // Kod för att modifiera parkeringsplats (inte implementerat)
                        break;
                    case "5":
                        // Kod för att hantera tilläggsavgifter (inte implementerat)
                        break;
                    case "6":
                        return;  // Gå tillbaka till huvudmenyn
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

                // Lägg till en paus innan man återvänder till menyn
                TillbakaTillMeny();
            }
        }

        private void ParkeraFordon()
        {
            Console.Clear();  // Rensa skärmen innan parkering
            Console.Write("Ange registreringsnummer: ");
            string registreringsnummer = Console.ReadLine();
            Console.Write("Ange färg: ");
            string färg = Console.ReadLine();
            Console.Write("Ange typ (bil, buss, motorcykel): ");
            string typ = Console.ReadLine();

            Fordon fordon = null;

            switch (typ.ToLower())
            {
                case "bil":
                    Console.Write("Är det en elbil? (ja/nej): ");
                    bool elbil = Console.ReadLine().ToLower() == "ja";
                    fordon = new Bil(registreringsnummer, färg, elbil);
                    break;
                case "buss":
                    Console.Write("Ange antal passagerare: ");
                    int antalPassagerare = int.Parse(Console.ReadLine());
                    fordon = new Buss(registreringsnummer, färg, antalPassagerare);
                    break;
                case "motorcykel":
                    Console.Write("Ange märke: ");
                    string märke = Console.ReadLine();
                    fordon = new Motorcykel(registreringsnummer, färg, märke);
                    break;
                default:
                    Console.WriteLine("Ogiltig typ.");
                    return;
            }

            Console.Write("Ange hur länge du vill parkera (i sekunder): ");
            double varaktighet = double.Parse(Console.ReadLine());
            Console.Clear();
            string resultat = parkeringshus.ParkeraFordon(fordon, varaktighet);
            parkeringshus.VisaParkering();
            Console.WriteLine(resultat);
            TillbakaTillMeny();
        }

        private void CheckaUtFordon()
        {
            Console.Clear();  // Rensa skärmen innan check out
            Console.Write("Ange registreringsnummer för att checka ut: ");
            string registreringsnummer = Console.ReadLine();
            parkeringshus.CheckaUtFordon(registreringsnummer);
            TillbakaTillMeny();
        }

        
        private void TillbakaTillMeny()
        {
            Console.WriteLine("Tryck på en tangent för att gå tillbaka till menyn.");
            Console.ReadKey();
        }
    }
}   