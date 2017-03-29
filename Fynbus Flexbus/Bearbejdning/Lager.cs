using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus
{
    public sealed class Lager
    {
        public List<Rute> Ruter;
        public List<Byder> Byder;

        static readonly Lager lager = new Lager();

        private Lager()
        {
            Ruter = new List<Rute>();
            Byder = new List<Byder>();
        }
        public static Lager HentUdgave()
        {
            return lager;
        }
        public void HentLister(List<Rute> ruter, List<Byder> byder)
        {
            Ruter = ruter;
            Byder = byder;
        }
    }
}
