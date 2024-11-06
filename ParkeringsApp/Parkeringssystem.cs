using System;

namespace ParkeringsApp
{
    public class Parkeringssystem
    {
        private Parkeringshus parkeringshus = new Parkeringshus();

        public void Kör()
        {
            while (true)
            {
                Console.WriteLine("1. Parkera fordon");
                Console.WriteLine("2. Lista fordon");
                Console.WriteLine("3. Visa parkering");
                Console.WriteLine("4. Checka ut fordon");
                Console.WriteLine("5. Avsluta");
                Console.Write("Välj alternativ: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        ParkeraFordon();
                        break;
                    case "2":
                        parkeringshus.ListaFordons();
                        break;
                    case "3":
                        parkeringshus.VisaParkering();  // Visa parkeringens status
                        break;
                    case "4":
                        CheckaUtFordon();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        private void ParkeraFordon()
        {
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

            string resultat = parkeringshus.ParkeraFordon(fordon, varaktighet);
            Console.WriteLine(resultat);
        }

        private void CheckaUtFordon()
        {
            Console.Write("Ange registreringsnummer för att checka ut: ");
            string registreringsnummer = Console.ReadLine();
            parkeringshus.CheckaUtFordon(registreringsnummer);
        }
    }
}
