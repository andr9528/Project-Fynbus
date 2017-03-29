using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus.Bearbejdning
{
    public class Logik
    {
        Lager lager = Lager.HentUdgave();
        List<Rute> Ruter = new List<Rute>();
        List<Byder> Byderne = new List<Byder>();

        public void Import(string filPlaceringForTilbud, string filPlaceringForByder)
        {
            Import import = new Import(filPlaceringForTilbud, filPlaceringForByder);
            Ruter = import.HentRuter();
            Byderne = import.HentByder();
        }
        public void SendListerTilLager()
        {
            lager.HentLister(Ruter, Byderne);
        }
    }
}
