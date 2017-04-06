using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fynbus_Flexbus;

namespace Tests
{
    [TestClass]
    public class LogikTest
    {
        //André's Stationær Import
        Logik logik = new Logik(@"C:\Users\andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\14_FG5_Tilbudsblanket_tilpasset skabelon_annonymiseret i csv_format.csv", @"C:\Users\Andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\4_FG5_Stamoplysninger_tilpasset skabelon_annonymiseret i csv_format.csv");
        //André's Bærbar Import
        //Logik logik = new Logik(@"C:\Users\Andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\14_FG5_Tilbudsblanket_tilpasset skabelon_annonymiseret i csv_format.csv", @"C:\Users\Andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\4_FG5_Stamoplysninger_tilpasset skabelon_annonymiseret i csv_format.csv");
        Lager lager = Lager.HentUdgave();

        [TestMethod]
        public void TestLogik1()
        {
            int test = 0;

            foreach (var rute in logik.Ruter)
            {
                if (rute.HarVinder)
                {
                    test++;
                }
            }
            Assert.AreEqual(logik.TotalMaxAntalUdfyldteRuter, test);
        }
        [TestMethod]
        public void TestLogik2()
        {
            Assert.AreEqual(69, logik.TotalMaxAntalVogne);
        }
        [TestMethod]
        public void TestLager1()
        {
            Assert.AreEqual(172, lager.Ruter.Count);
        }

    }
}
