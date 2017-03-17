namespace Fynbus_Flexbus
{
    public class Vogn
    {
        public string Type { get; set; }
        public string VognID { get; set; }
        public int Antal { get; set; }
        public string Ejer { get; set; }

        public Vogn(string vognID, string ejer, int antal = 1)
        {
            VognID = vognID;
            Ejer = ejer;
            Antal = antal;
        }
    }
}