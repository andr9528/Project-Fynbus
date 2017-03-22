﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fynbus_Flexbus;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class IntegrationTest
    {
        List<Bydder> listBydder = new List<Bydder>();
        List<Rute> listRuter = new List<Rute>();

        Bydder bydder1 = new Bydder("1", "André", "Skole", "stuff1@mail.com");
        Bydder bydder2 = new Bydder("2", "Peter", "Skole", "stuff2@mail.com");
        Bydder bydder3 = new Bydder("3", "Simon", "Skole", "stuff3@mail.com");
        Bydder bydder4 = new Bydder("4", "Christian", "Skole", "stuff4@mail.com");
        Bydder bydder5 = new Bydder("5", "Nicolai", "Skole", "stuff5@mail.com");

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

            Tilbud[] tilbud = new Tilbud[] { tilbud1, tilbud2, tilbud3, tilbud4, tilbud5 };
            Bydder[] bydder = new Bydder[] { bydder1, bydder2, bydder3, bydder4, bydder5 };

            rute1.Tilbud.AddRange(tilbud);

            listRuter.Add(rute1);
            listBydder.AddRange(bydder);
        }
        [TestCleanup]
        public void Clean()
        {
            foreach (Rute rute in listRuter)
            {
                rute.Tilbud.Clear();
                rute.Top3.Clear();
            }
            listBydder.Clear();
            listRuter.Clear();
        }
    }
}