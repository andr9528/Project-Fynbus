using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus.Opbevaring
{
    public class Import
    {
        public List<Bydder> Bydder = new List<Bydder>();
        public List<Tilbud> Tilbud = new List<Tilbud>();
        public List<Rute> Ruter = new List<Rute>();


        /*
         * Tjekker listen af ruter igennem for at om der allerede eksistere en rute med dette ruteNummer
         * Retunere falsk, hvis der ikke er nogle med dette ruteNummer, og retunere sandt hvis der allerede er en rute med dette ruteNummer
         *      Tager imod en rute, hvis ruteNummer den bruger til at tjekke med
         */
        private bool TjekOmRuteEksistere(Rute rute)
        {
            bool retur = false;

            return retur;
        }
        /*
         * Tjekker listen af byddere igennem for at se om der allerede eksistere en person med dette ID
         * Retunere falsk, hvis der ikke er nogle med dette ID, og retunere sandt hvis der allerede er et tilbud med ID'en
         *      Tager imod en bydder, hvis ID den bruger til at tjekke med.
         */
        private bool TjekOmBydderEksistere(Bydder bydder)
        {
            bool retur = false;

            return retur;
        }
        /*
         * Tjekker listen af tilbud igennem for at se om der allerede eksistere et tilbud med dette ID
         * Retunere falsk, hvis der ikke er nogle med dette ID, og retunere sandt hvis der allerede er et tilbud med ID'en
         *      Tager imod et tillbud, hvis ID den bruger til at tjekke med.
         */
        private bool TjekOmTilbudEksistere(Tilbud tilbud)
        {
            bool retur = false;

            return retur;
        }
        /*
         * Går gennem alle ruter, og bind de tilsvarende tilbud til ruterne
         *      - Skal kørers før listerne forlader denne klasse. 
         */
        private void BindTilbudTilRuter()
        {

        }
        /*
         * Opretter vognene som hver bydder har udfra informationerne i filen med bydder
         *      Tager imod en bydderID, for at finde hvilken linje den skal arbejde med, samt filplaceringen for bydderne.
         */
        private List<Vogn> ImportVogneForBydder(string bydderID, string filPlaceringForBydder)
        {
            List<Vogn> returListe = new List<Vogn>();

            return returListe;
        }
        /*
         * Opretter ruter udfra de unike værdier der er i garantivognsnummer kolenen inde i filen med tilbud
         */
        private void ImportRuter(string filPlaceringForTilbud)
        {

        }
        /*
         * Opretter byddere udfra de unike bydder der er i filen med stamoplysninger
         */
        private void ImportBydder(string filPlaceringForBydder)
        {

        }
        /*
         * Opretter tilbud udfra de unike tilbud der er i filen med tilbud
         */
        private void ImportTilbud(string filPlaceringForTilbud)
        {

        }
        /*
         * Køre alle metoderne i korrekt rækkefølge, når der initieres en ny import
         */
        public Import(string filPlaceringForTilbud, string filPlaceringForBydder)
        {
            ImportRuter(filPlaceringForTilbud);
            ImportBydder(filPlaceringForBydder);
            ImportTilbud(filPlaceringForTilbud);
            BindTilbudTilRuter();
        }
    }
}
