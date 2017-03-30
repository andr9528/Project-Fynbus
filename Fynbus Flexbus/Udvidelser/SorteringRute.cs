using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus
{
    public class SorteringRute : IComparer<Rute>
    {
        public int Compare(Rute o1, Rute o2)
        {
            if (o1.FørstePlads.ForskelTilNæste > o2.FørstePlads.ForskelTilNæste)
            {
                return -1;
            }
            else if (o1.FørstePlads.ForskelTilNæste < o2.FørstePlads.ForskelTilNæste)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
