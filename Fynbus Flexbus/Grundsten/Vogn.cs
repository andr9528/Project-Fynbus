﻿namespace Fynbus_Flexbus
{
    public class Vogn
    {
        public string Type { get; set; }
        public int Antal { get; set; }
        public string Ejer { get; set; }

        public Vogn(string ejer, int antal = 1)
        {
            Ejer = ejer;
            Antal = antal;
        }
    }
}