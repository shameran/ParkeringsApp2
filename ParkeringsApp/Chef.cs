using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkeringsApp
{
    public class Chef
    {
        public void VisaParkeringshus(Parkeringshus parkeringshus)
        {
            Console.WriteLine("Översikt av parkeringshuset:");
            parkeringshus.ListaFordons();
        }
    }
}
