using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fynbus_Flexbus
{
    public class Import
    {
        List<Byder> Byderne = new List<Byder>();
        List<Tilbud> Tilbudene = new List<Tilbud>();
        List<Rute> Ruter = new List<Rute>();
        
        /*
         * Tjekker listen af ruter igennem for at om der allerede eksistere en rute med dette ruteNummer
         * Retunere falsk, hvis der ikke er nogle med dette ruteNummer, og retunere sandt hvis der allerede er en rute med dette ruteNummer
         *      Tager imod en rute, hvis ruteNummer den bruger til at tjekke med
         */
        private bool TjekOmRuteEksistere(Rute ruten)
        {
            bool retur = false;
            foreach (Rute rute in Ruter)
            {
                if (rute.RuteNummer == ruten.RuteNummer)
                {
                    retur = true;
                }
            }
            return retur;
        }
        /*
         * Tjekker listen af byddere igennem for at se om der allerede eksistere en person med dette ID
         * Retunere falsk, hvis der ikke er nogle med dette ID, og retunere sandt hvis der allerede er et tilbud med ID'en
         *      Tager imod en bydder, hvis ID den bruger til at tjekke med.
         */
        private bool TjekOmByderEksistere(Byder byderen)
        {
            bool retur = false;
            foreach (Byder byder in Byderne)
            {
                if (byder.ByderID == byderen.ByderID)
                {
                    retur = true;
                }
            }
            return retur;
        }
        /*
         * Tjekker listen af tilbud igennem for at se om der allerede eksistere et tilbud med dette ID
         * Retunere falsk, hvis der ikke er nogle med dette ID, og retunere sandt hvis der allerede er et tilbud med ID'en
         *      Tager imod et tillbud, hvis ID den bruger til at tjekke med.
         */
        private bool TjekOmTilbudEksistere(Tilbud tilbudet)
        {
            bool retur = false;
            foreach (Tilbud tilbud in Tilbudene)
            {
                if (tilbud.TilbudID == tilbudet.TilbudID)
                {
                    retur = true;
                }
            }
            return retur;
        }
        /*
         * Går gennem alle ruter, og bind de tilsvarende tilbud til ruterne
         *      - Skal kørers før listerne forlader denne klasse. 
         */
        private void BindTilbudTilRuter()
        {
            foreach (Rute rute in Ruter)
            {
                foreach (Tilbud tilbud in Tilbudene)
                {
                    if (rute.RuteNummer == tilbud.RuteNummer)
                    {
                        rute.Tilbud.Add(tilbud);
                    }
                }
            }
        }
        /*
         * Opretter vognene som hver bydder har udfra informationerne i filen med bydder
         *      Tager imod en bydderID, for at finde hvilken linje den skal arbejde med, samt filplaceringen for bydderne.
         *      --- Metoden kræver at navnet på byderen står i anden colone
         */
        private List<Vogn> ImportVogneForByder(string byderID, string filPlaceringForByder)
        {
            List<Vogn> returListe = new List<Vogn>();

            var læs = File.ReadAllLines(filPlaceringForByder);

            for (int y = 0; y < læs.Length; y++)
            {
                if (y == 0)
                {
                    continue;
                }
                if (læs[y].ToString().Split(';')[0] == byderID)
                {
                    string navn = læs[y].ToString().Split(';')[1];

                    for (int x = 4; x < læs[y].ToString().Split(';').Length; x++) // finder antal vogne at filen kender til, og køre 1 gang per vogn
                    {
                        string type = læs[0].ToString().Split(';')[x];
                        int antal = -1;
                        int.TryParse(læs[y].ToString().Split(';')[x], out antal);

                        if (antal == -1)
                        {
                            antal = 0;
                        }

                        Vogn vogn = new Vogn(type, navn, antal);

                        returListe.Add(vogn); 
                    }
                }
            }

            return returListe;
        }
        /*
         * Opretter ruter udfra de unike værdier der er i garantivognsnummer kolenen inde i filen med tilbud
         */
        private void ImportRuter(string filPlaceringForTilbud)
        {
            var læs = File.ReadAllLines(filPlaceringForTilbud);

            for (int y = 0; y < læs.Length; y++)
            {
                if (y == 0)
                {
                    continue;
                }
                int ruteNummer = -1;
                bool omdannerTjek = int.TryParse(læs[y].ToString().Split(';')[1], out ruteNummer);

                if (omdannerTjek == false || ruteNummer == -1)
                {
                    throw new Exception("Importering af Rute numrer slog fejl, tjek om der er en rutenummer celle som indeholder andet end tal på linje " + y);
                }
                else
                {
                    Rute rute = new Rute(ruteNummer);

                    if (TjekOmRuteEksistere(rute) == false)
                    {
                        Ruter.Add(rute);
                    }
                }
            }
        }
        /*
         * Opretter byddere udfra de unike bydder der er i filen med stamoplysninger
         */
        private void ImportByder(string filPlaceringForByder)
        {
            var læs = File.ReadAllLines(filPlaceringForByder);

            for (int y = 0; y < læs.Length; y++)
            {
                if (y == 0)
                {
                    continue;
                }
                string byderID = læs[y].ToString().Split(';')[0];
                string navn = læs[y].ToString().Split(';')[1];
                string firma = læs[y].ToString().Split(';')[2];
                string mail = læs[y].ToString().Split(';')[3];

                Byder byder = new Byder(byderID, navn, firma, mail);
                byder.Vogne.AddRange(ImportVogneForByder(byderID, filPlaceringForByder));

                if (TjekOmByderEksistere(byder) == false)
                {
                    Byderne.Add(byder);
                }
            }
        }
        /*
         * Opretter tilbud udfra de unike tilbud der er i filen med tilbud
         */
        private void ImportTilbud(string filPlaceringForTilbud)
        {
            var læs = File.ReadAllLines(filPlaceringForTilbud);

            for (int y = 0; y < læs.Length; y++)
            {
                if (y == 0)
                {
                    continue;
                }
                string tilbudID = læs[y].ToString().Split(';')[0];
                int ruteNummer = -1;
                double timePris = -1;
                Byder byder = null;
                int byderPrioritet = -1;
                int rutePrioritet = -1;

                bool parseTjekRuteNummer = int.TryParse(læs[y].ToString().Split(';')[1], out ruteNummer);
                bool parseTjekTimePris = double.TryParse(læs[y].ToString().Split(';')[2], out timePris);
                bool parseTjekByderPrioritet = int.TryParse(læs[y].ToString().Split(';')[6], out byderPrioritet);
                bool parseTjekRutePrioritet = int.TryParse(læs[y].ToString().Split(';')[7], out rutePrioritet);

                foreach (Byder byderen in Byderne)
                {
                    if (byderen.Navn == læs[y].ToString().Split(';')[3]
                        && byderen.Firma == læs[y].ToString().Split(';')[4]
                        && byderen.Mail == læs[y].ToString().Split(';')[5])
                    {
                        byder = byderen;
                    }
                }
                if (byder == null)
                {
                    throw new Exception("Der blev ikke funedet en byder, med tilsvarende informationer, problemet opstod på linje " + y);
                }
                if (parseTjekByderPrioritet == false)
                {
                    byderPrioritet = 0;
                }
                if (parseTjekRutePrioritet == false)
                {
                    rutePrioritet = 0;
                }
                if (parseTjekRuteNummer == false)
                {
                    throw new Exception("Omdannelsen af et Rute numrer slog fejl, tjek om der er en rutenummer celle som indeholder andet end tal på linje " + y);
                }
                if (parseTjekTimePris == false)
                {
                    throw new Exception("Omdannelsen af en Time pris slog fejl, tjek om der er en time pris celle som indeholder andet end tal på linje " + y);
                }

                Tilbud tilbud = new Tilbud(tilbudID, ruteNummer, timePris, byder, byderPrioritet, rutePrioritet);

                if (TjekOmTilbudEksistere(tilbud) == false)
                {
                    Tilbudene.Add(tilbud);
                }
            }
        }
        /*
         * Køre alle metoderne i korrekt rækkefølge, når der initieres en ny import
         */
        public Import(string filPlaceringForTilbud, string filPlaceringForByder)
        {
            ImportRuter(filPlaceringForTilbud);
            ImportByder(filPlaceringForByder);
            ImportTilbud(filPlaceringForTilbud);
            BindTilbudTilRuter();
        }
        public List<Rute> HentRuter()
        {
            return Ruter;
        }
        public List<Byder> HentByder()
        {
            return Byderne;
        }
    }
}
