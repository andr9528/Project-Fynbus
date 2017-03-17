﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fynbus_Flexbus;

namespace Tests
{
    [TestClass]
    public class GrundstenTests
    {
        Bydder bydder1 = new Bydder("1", "André", "stuff1@mail.com");
        Bydder bydder2 = new Bydder("2", "Peter", "stuff2@mail.com");
        Bydder bydder3 = new Bydder("3", "Simon", "stuff3@mail.com");
        Bydder bydder4 = new Bydder("4", "Christian", "stuff4@mail.com");
        Bydder bydder5 = new Bydder("5", "Nicolai", "stuff5@mail.com");

        Rute rute1;

        [TestInitialize]
        public void Setup()
        {
            rute1 = new Rute(1);

            Tilbud tilbud1 = new Tilbud("1", 1, 3, bydder1); // plads 3
            Tilbud tilbud2 = new Tilbud("2", 1, 5, bydder2); // plads 4
            Tilbud tilbud3 = new Tilbud("3", 1, 1, bydder3); // plads 0
            Tilbud tilbud4 = new Tilbud("4", 1, 2, bydder4); // plads 2
            Tilbud tilbud5 = new Tilbud("5", 1, 2, bydder5); // plads 1

            Tilbud[] tilbud = new Tilbud[] { tilbud1, tilbud2, tilbud3, tilbud4,tilbud5 };

            rute1.Tilbud.AddRange(tilbud);
        }

        //[TestMethod]
        //public void TestSortering1()
        //{
        //    rute1.SorterTilbud();

        //    Assert.AreEqual(5, rute1.Tilbud.Count);
        //}
        //[TestMethod]
        //public void TestSortering2()
        //{
        //    rute1.SorterTilbud();

        //    Assert.AreEqual(1, rute1.Tilbud[0].TimePris);
        //}
        //[TestMethod]
        //public void TestSortering3()
        //{
        //    rute1.SorterTilbud();

        //    Assert.AreEqual(bydder4, rute1.Tilbud[2].Bydder);
        //}
    }
}
