using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus
{
    public class Udvidelser : IComparer<Tilbud>
    {
        public int Compare(Tilbud o1, Tilbud o2)
        {
            if (o1.TimePris > o2.TimePris)
            {
                return -1;
            }
            else if (o1.TimePris < o2.TimePris)
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
