using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus
{
    public class Tilbud
    {
        public string TilbudID { get; set; }
        public int RuteNummer { get; set; }
        public double TimePris { get; set; }
        public double ForskelTilNæste { get; set; } // værdien der indeholde time pris forskellen til det næste tilbud
        public Bydder Bydder { get; set; }

        public Tilbud(string tilbudID, int ruteNummber, double timePris, Bydder bydder)
        {
            TilbudID = tilbudID;
            RuteNummer = ruteNummber;
            TimePris = timePris;
            Bydder = bydder;
        }
        
    }
}
