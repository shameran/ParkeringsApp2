using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkeringsApp;
using System;

namespace ParkeringsAppTests
{
    [TestClass]
    public class ParkeringshusTests
    {
        private Parkeringshus parkering;
        private Fordon bil;

        [TestInitialize]
        public void Setup()
        {
            parkering = new Parkeringshus();
            bil = new Bil("ABC123", "Volvo", true);
        }

        [TestMethod]
        public void TestIngenInmatning()
        {
            parkering.ParkeraFordon(bil, 3600);
            var resultat = parkering.ParkeraFordon(bil, 3600);
            Assert.IsTrue(resultat.Contains("Ingen ledig plats"), "F�rv�ntar sig att det ska vara en full parkering");
        }

        [TestMethod]
        public void TestBokst�verIst�lletF�rSiffror()
        {
            try
            {
                var resultat = parkering.FlyttaFordon("ABC123", 'A');
                Assert.Fail("F�rv�ntar sig ett undantag f�r ogiltig inmatning");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Ogiltig parkering", ex.Message, "F�rv�ntar sig att ett felmeddelande returneras");
            }
        }

        [TestMethod]
        public void TestKorrektaBerakningar()
        {
            var parkeringstid = 3600;
            bil.Parkeringstid = parkeringstid;

            double f�rv�ntadPris = 10 * 1;

            var resultat = bil.Ber�knaPris();

            Assert.AreEqual(f�rv�ntadPris, resultat, "Parkeringskostnaden ska vara korrekt.");
        }

        [TestMethod]
        public void TestTaBortFordon()
        {
            parkering.ParkeraFordon(bil, 3600);
            bool fordonBorttagen = parkering.TaBortFordon("ABC123");
            Assert.IsTrue(fordonBorttagen, "Fordonet ska tas bort korrekt.");
        }

        [TestMethod]
        public void TestL�ggTillB�ter()
        {
            parkering.ParkeraFordon(bil, 3600);
            parkering.L�ggTillB�ter("ABC123", 500);

            Assert.AreEqual(500, bil.B�ter, "B�terna ska vara korrekt tillagda.");
        }
    }
}
