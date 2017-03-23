using System.Collections.Generic;

namespace Fynbus_Flexbus
{
    public class Bydder
    {
        public List<Vogn> Vogne = new List<Vogn>();
        public string BydderID { get; set; } // seqno
        public string Navn { get; set; }
        public string Mail { get; set; }
        public string Firma { get; set; }
        public int MaxAntalVogne
        {
            get
            {
                int output = 0;
                foreach (var vogn in Vogne)
                {
                    output += vogn.Antal;
                }
                return output;
            } }
        

        public Bydder(string bydderID, string navn, string firma,  string mail)
        {
            BydderID = bydderID;
            Navn = navn;
            Firma = firma;
            Mail = mail;
        }
    }
}