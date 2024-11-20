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
                Console.Clear(); 
                Console.WriteLine("ParkeringsVakt:");
                parkeringshus.VisaParkering();
                Console.WriteLine("1. Se/redigera parkeringstid");
                Console.WriteLine("2. Se fordonsinformation");
                Console.WriteLine("3. Modifiera parkeringsplats");
                Console.WriteLine("4. Hantera böter");
                Console.WriteLine("5. Tillbaka till huvudmenyn");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        parkeringshus.VisaParkering();
                        break;
                    case "2":
                        parkeringshus.ListaFordons();
                        break;
                    case "3":
                        ModifieraParkeringsplats();  
                        break;
                    case "4":
                        HanteraBöter();
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

        
        private void ModifieraParkeringsplats()
        {
            Console.Clear();
            Console.WriteLine("Modifiera Parkeringsplats");
            Console.WriteLine("1. Flytta fordon");
            Console.WriteLine("2. Ta bort fordon från parkeringen");
            Console.Write("Välj alternativ: ");
            string val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    FlyttaFordon();
                    break;
                case "2":
                    TaBortFordon();
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }
        }

        
        private void FlyttaFordon()
        {
            Console.Clear();
            Console.Write("Ange registreringsnummer för fordonet du vill flytta: ");
            string registreringsnummer = Console.ReadLine();
            Console.Write("Ange ny parkeringsplats (t.ex. A1, B2): ");
            string nyPlats = Console.ReadLine().ToUpper();  

            
            int platsIndex = OmvandlaPlatsTillIndex(nyPlats);

            if (platsIndex != -1)  
            {
                if (parkeringshus.FlyttaFordon(registreringsnummer, platsIndex))
                {
                    Console.WriteLine($"Fordonet {registreringsnummer} har flyttats till {nyPlats}.");
                }
                else
                {
                    Console.WriteLine("Fordonet kunde inte flyttas. Kontrollera registreringsnumret och parkeringens tillgänglighet.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltig plats. Vänligen ange en giltig plats (t.ex. A1, B2).");
            }

            TillbakaTillMeny();
        }

        
        private int OmvandlaPlatsTillIndex(string plats)
        {
            if (plats.Length < 2) return -1; 

            char rad = plats[0];  
            char kolumn = plats[1];  

            // Kontrollera att raden är en giltig bokstav 
            if (rad < 'A' || rad > 'E') return -1;

            // Kontrollera att kolumnen är ett giltigt nummer 
            if (kolumn < '1' || kolumn > '5') return -1;

            // Omvandla raden till ett index
            int radIndex = rad - 'A';

            // Omvandla kolumnen till ett index 
            int kolumnIndex = kolumn - '1';

            // totala indexet i listan 
            int platsIndex = radIndex * 5 + kolumnIndex;
            return platsIndex;
        }



        
        private void TaBortFordon()
        {
            Console.Clear();
            Console.Write("Ange registreringsnummer för fordonet du vill ta bort: ");
            string registreringsnummer = Console.ReadLine();

            
            if (parkeringshus.TaBortFordon(registreringsnummer))
            {
                Console.WriteLine($"Fordonet {registreringsnummer} har tagits bort från parkeringen.");
            }
            else
            {
                Console.WriteLine("Fordonet kunde inte tas bort. Kontrollera registreringsnumret.");
            }
            TillbakaTillMeny();
        }

        
        private void HanteraBöter()
        {
            Console.Clear();
            Console.WriteLine("Hantera böter för ett fordon");
            Console.Write("Ange registreringsnummer för fordonet: ");
            string registreringsnummer = Console.ReadLine();

            Console.WriteLine("Vill du (1) Lägga till, (2) Modifiera eller (3) Ta bort böter?");
            string val = Console.ReadLine();
            double bötesbelopp = 0;

            if (val == "1" || val == "2")
            {
                Console.Write("Ange belopp: ");
                bötesbelopp = double.Parse(Console.ReadLine());
            }

            if (val == "1")
            {
                parkeringshus.LäggTillBöter(registreringsnummer, bötesbelopp);
            }
            else if (val == "2")
            {
                parkeringshus.ModifieraBöter(registreringsnummer, bötesbelopp);
            }
            else if (val == "3")
            {
                parkeringshus.TaBortBöter(registreringsnummer);
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }

        private void ParkeraFordon()
        {
            Console.Clear();  
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
            Console.Clear();  
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
