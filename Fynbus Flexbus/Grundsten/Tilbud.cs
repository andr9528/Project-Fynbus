using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus
{
    public class Tilbud
    {
        public string TilbudID { get; set; } // seqno
        public bool VinderStatus { get; set; }
        public int RuteNummer { get; set; }
        public double TimePris { get; set; }
        public double ForskelTilNæste { get; set; } // værdien der indeholde time pris forskellen til det næste tilbud
        public Byder Byder { get; set; }
        public int ByderPrioritet { get; set; } // er muligvis unødvendig
        public int RutePrioritet { get; set; } // er muligvis unødvendig

        public Tilbud(string tilbudID, int ruteNummer, double timePris, Byder byder, int bydderPrioritet = 0, int rutePrioritet = 0)
        {
            TilbudID = tilbudID;
            RuteNummer = ruteNummer;
            TimePris = timePris;
            Byder = byder;
            ByderPrioritet = bydderPrioritet;
            RutePrioritet = rutePrioritet;
        }
        
    }
}
