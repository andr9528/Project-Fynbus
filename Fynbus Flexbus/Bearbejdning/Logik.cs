using Fynbus_Flexbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fynbus_Flexbus
{
    public class Logik
    {
        Lager lager = Lager.HentUdgave();

        // fjern public når der ikke testes
        public List<Rute> Ruter = new List<Rute>();
        public List<Byder> Byderne = new List<Byder>();
        private int TotalMaxAntalVogne
        {
            get { int retur = 0;

                foreach (var byder in Byderne)
                {
                    retur += byder.MaxAntalVogne;
                }

                return retur;
            }
        }

        public Logik(string filPlaceringForTilbud, string filPlaceringForByder)
        {
            Import(filPlaceringForTilbud, filPlaceringForByder);
            SorterRuter();

            TælFørstePladser();
            UpdaterBydereITilbud();
            FindVinderFra1Plads();

            TælAndenpladser();
            UpdaterBydereITilbud();
            FindVinderFra2Plads();

            TælTredjepladser();
            UpdaterBydereITilbud();
            FindVinderFra3Plads();
            
            SendListerTilLager();
        }

        private void SorterRuter()
        {
            IComparer<Rute> sortering = new SorteringRute();

            Ruter.Sort(sortering);
        }

        private void FindVinderFra3Plads()
        {
            foreach (var rute in Ruter)
            {
                if (rute.HarVinder == false)
                {
                    if (rute.TredjePlads.Byder.Tredjepladser <= rute.TredjePlads.Byder.MaxAntalVogne)
                    {
                        rute.Vinder = rute.TredjePlads;

                        foreach (var byder in Byderne)
                        {
                            if (rute.Vinder.Byder.Navn == byder.Navn
                                && rute.Vinder.Byder.Firma == byder.Firma
                                && rute.Vinder.Byder.Mail == byder.Mail)
                            {
                                byder.LedigeVogne--;
                                UpdaterBydereITilbud();
                            }
                        }
                    }
                }

            }
            for (int i = 0; i < Ruter.Count; i++)
            {
                if (Ruter[i].HarVinder == false)
                {
                    if (Ruter[i].TredjePlads.Byder.LedigeVogne > 0)
                    {
                        Ruter[i].Vinder = Ruter[i].TredjePlads;

                        foreach (var byder in Byderne)
                        {
                            if (Ruter[i].Vinder.Byder.Navn == byder.Navn
                                && Ruter[i].Vinder.Byder.Firma == byder.Firma
                                && Ruter[i].Vinder.Byder.Mail == byder.Mail)
                            {
                                byder.LedigeVogne--;
                                UpdaterBydereITilbud();
                            }
                        }
                    }
                }
            }
        }

        private void TælTredjepladser()
        {
            foreach (var byder in Byderne)
            {
                foreach (var rute in Ruter)
                {
                    if (rute.HarVinder == false)
                    {
                        if (byder.Navn == rute.TredjePlads.Byder.Navn
                            && byder.Firma == rute.TredjePlads.Byder.Firma
                            && byder.Mail == rute.TredjePlads.Byder.Mail)
                        {
                            byder.Førstepladser++;
                        }
                    }
                }
            }
        }

        private void FindVinderFra2Plads()
        {
            foreach (var rute in Ruter)
            {
                if (rute.HarVinder == false)
                {
                    if (rute.AndenPlads.Byder.Andenpladser <= rute.AndenPlads.Byder.MaxAntalVogne)
                    {
                        rute.Vinder = rute.AndenPlads;

                        foreach (var byder in Byderne)
                        {
                            if (rute.Vinder.Byder.Navn == byder.Navn
                                && rute.Vinder.Byder.Firma == byder.Firma
                                && rute.Vinder.Byder.Mail == byder.Mail)
                            {
                                byder.LedigeVogne--;
                                UpdaterBydereITilbud();
                            }
                        }
                    }
                }
                
            }
            for (int i = 0; i < Ruter.Count; i++)
            {
                if (Ruter[i].HarVinder == false)
                {
                    if (Ruter[i].AndenPlads.Byder.LedigeVogne > 0)
                    {
                        Ruter[i].Vinder = Ruter[i].AndenPlads;

                        foreach (var byder in Byderne)
                        {
                            if (Ruter[i].Vinder.Byder.Navn == byder.Navn
                                && Ruter[i].Vinder.Byder.Firma == byder.Firma
                                && Ruter[i].Vinder.Byder.Mail == byder.Mail)
                            {
                                byder.LedigeVogne--;
                                UpdaterBydereITilbud();
                            }
                        }
                    }
                }
            }
        }

        private void TælAndenpladser()
        {
            foreach (var byder in Byderne)
            {
                foreach (var rute in Ruter)
                {
                    if (rute.HarVinder == false)
                    {
                        if (byder.Navn == rute.AndenPlads.Byder.Navn
                            && byder.Firma == rute.AndenPlads.Byder.Firma
                            && byder.Mail == rute.AndenPlads.Byder.Mail)
                        {
                            byder.Førstepladser++;
                        }
                    }
                }
            }
        }

        private void FindVinderFra1Plads()
        {
            foreach (var rute in Ruter)
            {
                if (rute.FørstePlads.Byder.Førstepladser <= rute.FørstePlads.Byder.MaxAntalVogne)
                {
                    rute.Vinder = rute.FørstePlads;

                    foreach (var byder in Byderne)
                    {
                        if (rute.Vinder.Byder.Navn == byder.Navn
                            && rute.Vinder.Byder.Firma == byder.Firma
                            && rute.Vinder.Byder.Mail == byder.Mail)
                        {
                            byder.LedigeVogne--;
                            UpdaterBydereITilbud();
                        }
                    }
                }
            }
            for (int i = 0; i < Ruter.Count; i++)
            {
                if (Ruter[i].HarVinder == false)
                {
                    if (Ruter[i].FørstePlads.Byder.LedigeVogne > 0)
                    {
                        Ruter[i].Vinder = Ruter[i].FørstePlads;

                        foreach (var byder in Byderne)
                        {
                            if (Ruter[i].Vinder.Byder.Navn == byder.Navn
                                && Ruter[i].Vinder.Byder.Firma == byder.Firma
                                && Ruter[i].Vinder.Byder.Mail == byder.Mail)
                            {
                                byder.LedigeVogne--;
                                UpdaterBydereITilbud();
                            }
                        }
                    }
                }
            }
        }

        private void UpdaterBydereITilbud()
        {
            foreach (var byder in Byderne)
            {
                foreach (var rute in Ruter)
                {
                    foreach (var tilbud in rute.Tilbud)
                    {
                        if (byder.Navn == tilbud.Byder.Navn
                            && byder.Firma == tilbud.Byder.Firma
                            && byder.Mail == tilbud.Byder.Mail)
                        {
                            tilbud.Byder = byder;
                        }    
                    }
                }
            }
        }

        private void TælFørstePladser()
        {
            foreach (var byder in Byderne)
            {
                foreach (var rute in Ruter)
                {
                    if (byder.Navn == rute.FørstePlads.Byder.Navn
                            && byder.Firma == rute.FørstePlads.Byder.Firma
                            && byder.Mail == rute.FørstePlads.Byder.Mail)
                    {
                        byder.Førstepladser++;
                    }
                }
            }
        }

        private void Import(string filPlaceringForTilbud, string filPlaceringForByder)
        {
            Import import = new Import(filPlaceringForTilbud, filPlaceringForByder);
            Ruter = import.HentRuter();
            Byderne = import.HentByder();
        }
        private void SendListerTilLager()
        {
            lager.HentLister(Ruter, Byderne);
        }

    }
}
