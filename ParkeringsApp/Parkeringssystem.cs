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
                Console.WriteLine("2. ParkeringsVakt");
                Console.WriteLine("3. Chef");
                Console.WriteLine("4. Avsluta");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        KundMeny();
                        break;
                    case "2":
                        ParkeringsVakt();
                        break;
                    case "3":
                        Chef();
                        break;
                    case "4":
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

        private void Chef()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Chef");
                parkeringshus.VisaParkering();
                Console.WriteLine("3. Tillbaka till huvudmenyn");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        
                        break;
                    case "2":
                        
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                 }
             }
        }

        private void ParkeringsVakt()
        {
            while (true)
            {
                Console.Clear();  // Rensa skärmen när adminmenyn visas
                Console.WriteLine("ParkeringsVakt:");
                parkeringshus.VisaParkering();
                Console.WriteLine("1. Se/redigera parkeringstid");
                Console.WriteLine("2. Se fordonsinformation");
                Console.WriteLine("3. Modifiera parkeringsplats");
                Console.WriteLine("4. Hantera tilläggsavgifter");
                Console.WriteLine("5. Tillbaka till huvudmenyn");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        
                        parkeringshus.VisaParkering();
                        break;
                    case "2":
                        
                        break;
                    case "3":
                        parkeringshus.ListaFordons();  
                        break;
                    case "4":
                        break;
                    case "5":
                        return;  
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

                
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