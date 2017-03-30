using Fynbus_Flexbus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ImportTests
    {
        //André's Stationær Import
        //Import import = new Import(@"C:\Users\andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\14_FG5_Tilbudsblanket_tilpasset skabelon_annonymiseret i csv_format.csv", @"C:\Users\andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\4_FG5_Stamoplysninger_tilpasset skabelon_annonymiseret i csv_format.csv");
        //André's Bærbar Import
        Import import = new Import(@"C:\Users\Andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\14_FG5_Tilbudsblanket_tilpasset skabelon_annonymiseret i csv_format.csv", @"C:\Users\Andre\Google Drev\Skole - Google Drev\EAL\Project Fynbus\Fynbus New Code\Fynbus Flexbus\TestFiler\4_FG5_Stamoplysninger_tilpasset skabelon_annonymiseret i csv_format.csv");

        [TestMethod]
        public void TestImport1()
        {
            Assert.AreEqual(202, import.Tilbudene.Count);
        }
        [TestMethod]
        public void TestImport2()
        {
            Assert.AreEqual(7, import.Byderne.Count);
        }
        [TestMethod]
        public void TestImport3()
        {
            Assert.AreEqual("Type 2", import.Byderne[6].Vogne[0].Type);
            Assert.AreEqual(20, import.Byderne[6].Vogne[0].Antal);
        }
        [TestMethod]
        public void TestImport4()
        {
            Assert.AreEqual(172, import.Ruter.Count);
        }
    }
}
