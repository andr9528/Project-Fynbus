using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus
{
    public class Rute
    {
        public List<Tilbud> Tilbud = new List<Tilbud>();
        public List<Tilbud> Top3 = new List<Tilbud>();

        public int RuteNummer { get; set; }

        public Rute(int ruteNummer)
        {
            RuteNummer = ruteNummer;
        }

        /*
         * Nedenstående metode Sortere tilbudene efter time pris - lavst øverst/først
         * Efter første gang den er blevet kaldt, gør ekstra kald intet.
         *          Den laver i princippet kun en reverse reverse.
         */

        private void SorterTilbud() 
        {
            IComparer<Tilbud> sortering = new Udvidelser(); 

            Tilbud.Sort(sortering);

            Tilbud.Reverse();
        }

        /*
         * Nedenstående metode går gennem alle tilbudende og instiller deres værdi for forskelen til næste tilbud.
         * Hvis forskelen er 0 vil værdien blive sat til 0, og ved det sidste tilbud til værdien blive sat til -1, som en indikator
         * for at listen af tilbud er slut.
         *      - Må kun arbejde på en liste der er sorteret.
         */
        public void IndstilForskelIPris() // indstil til privat når den er blevet testet
        {
            SorterTilbud();

            for (int i = 0; i < Tilbud.Count; i++)
            {
                if (i + 1 > Tilbud.Count)
                {
                    Tilbud[i].ForskelTilNæste = -1;
                }
                else
                {
                    Tilbud[i].ForskelTilNæste = Tilbud[i + 1].TimePris - Tilbud[i].TimePris;
                }
            }
        }

        /*
         * Udtager de tre bedste tilbud og putter dem i en liste for dem selv.
         *      - Må kun arbejde på en liste der har fået indstillet pris forskel.
         */
         public void UdtagTop3() // indstil til privat når den er blevet testet
        {

        }

    }
}

