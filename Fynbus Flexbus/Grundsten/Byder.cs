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
        public int Vundne { get; set; }
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
        

        public Byder(string byderID, string navn, string firma,  string mail)
        {
            ByderID = byderID;
            Navn = navn;
            Firma = firma;
            Mail = mail;
        }
    }
}