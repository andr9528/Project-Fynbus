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

        public int RuteNummer { get; set; } // Garantivognsnummer
        public Tilbud FørstePlads { get
            {
                UdtagTop3();

                if (Top3.Count == 0)
                {
                    throw new Exception("Der er ingen tilbud på denne rute");
                }

                return Top3[0];
            } }
        public Tilbud AndenPlads { get
            {
                UdtagTop3();

                if (Top3.Count == 0)
                {
                    throw new Exception("Der er ingen tilbud på denne rute");
                }
                if (Top3.Count < 2)
                {
                    throw new Exception("Der er ingen anden plads på denne rute");
                }

                return Top3[1];
            } }
        public Tilbud TredjePlads { get
            {
                UdtagTop3();

                if (Top3.Count == 0)
                {
                    throw new Exception("Der er ingen tilbud på denne rute");
                }
                if (Top3.Count < 3)
                {
                    throw new Exception("Der er ingen tredje plads på denne rute");
                }

                return Top3[2];
            } }

        public Rute(int ruteNummer)
        {
            RuteNummer = ruteNummer;
        }

        /*
         * Nedenstående metode Sortere tilbudene efter time pris - lavst øverst/først
         * Efter første gang den er blevet kaldt, gør ekstra kald intet.
         *          Den laver i princippet kun en reverse reverse.
         */

        public void SorterTilbud() // Skal kun stå til public mens den bliver testet, ellers skal den stå til privat
        {
            IComparer<Tilbud> sortering = new Udvidelser(); 

            Tilbud.Sort(sortering);

            Tilbud.Reverse();
        }

        /*
         * Nedenstående metode går gennem alle tilbudende og instiller deres værdi for forskelen til næste tilbud.
         * Hvis forskelen er 0 vil værdien blive sat til 0, og ved det sidste tilbud til værdien blive sat til -1, som en indikator
         * for at listen af tilbud er slut.
         * Efter første gang den er blevet kaldt, gør ekstra kald intet.
         *      - Må kun arbejde på en liste der er sorteret.
         */
        public void IndstilForskelIPris() // Skal kun stå til public mens den bliver testet, ellers skal den stå til privat
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
         * Nedenstående metode udtager de tre bedste tilbud og putter dem i en liste for dem selv.
         * Efter første gang den er blevet kaldt, gør ekstra kald intet.
         *      - Må kun arbejde på en liste der har fået indstillet pris forskel.
         */
        public void UdtagTop3() // Skal kun stå til public mens den bliver testet, ellers skal den stå til privat
        {
            IndstilForskelIPris();

            Top3.Clear();

            for (int i = 0; i < 3; i++)
            {
                if (i >= Tilbud.Count)
                {
                    // gør inget, hvis der ikke er flere tilbud at tage af
                    // kommer ind når den forsøger at hente en plads som ikke eksistere.
                }
                else
                {
                    Top3.Add(Tilbud[i]);
                }
            }
        }

    }
}

