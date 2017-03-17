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
        public List<Bydder> Bydder;
        public List<Tilbud> Tilbud;

        static readonly Lager lager = new Lager();

        private Lager()
        {
            Ruter = new List<Rute>();
            Bydder = new List<Bydder>();
            Tilbud = new List<Tilbud>();
        }
        public static Lager HentUdgave()
        {
            return lager;
        }
        public void HentLister(List<Rute> ruter, List<Bydder> bydder, List<Tilbud> tilbud)
        {

        }
    }
}
