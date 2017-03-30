using System.Collections.Generic;

namespace Fynbus_Flexbus
{
    public class Byder
    {
        public List<Vogn> Vogne = new List<Vogn>();
        public string ByderID { get; set; } // seqno
        public string Navn { get; set; }
        public string Mail { get; set; }
        public string Firma { get; set; }
        public int Førstepladser { get; set; }
        public int Andenpladser { get; set; }
        public int Tredjepladser { get; set; }
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
        public int LedigeVogne { get; set; }
        
        //vogne har en default værdi på null, for at de ikke er nødvendige under test.
        public Byder(string byderID, string navn, string firma,  string mail, List<Vogn> vogne = null)
        {
            ByderID = byderID;
            Navn = navn;
            Firma = firma;
            Mail = mail;
            Vogne = vogne;
            LedigeVogne = MaxAntalVogne;
        }
    }
}