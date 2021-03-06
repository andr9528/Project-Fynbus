﻿using Fynbus_Flexbus;
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
        public int TotalMaxAntalVogne
        {
            get
            {
                int retur = 0;

                foreach (var byder in Byderne)
                {
                    retur += byder.MaxAntalVogne;
                }

                return retur;
            }
        }
        public int TotalMaxAntalUdfyldteRuter
        {
            get
            {
                int retur = 0;

                foreach (var byder in Byderne)
                {
                    if (byder.AntalBud > byder.MaxAntalVogne)
                    {
                        retur += byder.MaxAntalVogne;
                    }
                    else
                    {
                        retur += byder.AntalBud;
                    }
                }
                return retur;
            }
        }
            
        public Logik(string filPlaceringForTilbud, string filPlaceringForByder)
        {
            Import(filPlaceringForTilbud, filPlaceringForByder);
            SorterRuter();

            TælTilbudPerByder();
            
            FindVinderFra1Plads();
            FindVinderFra2Plads();
            FindVinderFra3Plads();
            FindRestVindere();

            GemVinderStatus();
            
            SendListerTilLager();
        }

        private void GemVinderStatus()
        {
            foreach (Rute rute in Ruter)
            {
                foreach (Tilbud tilbud in rute.Tilbud)
                {
                    if (rute.HarVinder == true)
                    {
                        if (rute.Vinder.Byder == tilbud.Byder)
                        {
                            tilbud.VinderStatus = true;
                        }
                        else
                        {
                            tilbud.VinderStatus = false;
                        }
                    }
                    else
                    {
                        tilbud.VinderStatus = false;
                    }
                }
            }
        }

        private void TælTilbudPerByder()
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
                            byder.AntalBud++;
                        }
                    }
                }
            }
            UpdaterBydereITilbud();
        }

        private void FindRestVindere()
        {
            foreach (var rute in Ruter)
            {
                int index = 3;
                while (rute.HarVinder == false && rute.Udtag(index) != null)
                {
                    if (rute.Udtag(index).Byder.LedigeVogne > 0)
                    {
                        rute.Vinder = rute.Udtag(index);

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
                    index++;
                }
            }
        }

        private void SorterRuter()
        {
            IComparer<Rute> sortering = new SorteringRute();

            Ruter.Sort(sortering);
        }

        private void FindVinderFra3Plads()
        {
            TælTredjepladser();
            foreach (var rute in Ruter)
            {
                if (rute.HarVinder == false)
                {
                    if (rute.TredjePlads != null)
                    {
                        if (rute.TredjePlads.Byder.Tredjepladser <= rute.TredjePlads.Byder.MaxAntalVogne)
                        {
                            if (rute.TredjePlads.Byder.LedigeVogne > 0)
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
                }

            }
            for (int i = 0; i < Ruter.Count; i++)
            {
                if (Ruter[i].HarVinder == false)
                {
                    if (Ruter[i].TredjePlads != null)
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
        }

        private void TælTredjepladser()
        {
            foreach (var byder in Byderne)
            {
                foreach (var rute in Ruter)
                {
                    if (rute.HarVinder == false)
                    {
                        if (rute.TredjePlads != null)
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
            UpdaterBydereITilbud();
        }

        private void FindVinderFra2Plads()
        {
            TælAndenpladser();
            foreach (var rute in Ruter)
            {
                if (rute.HarVinder == false)
                {
                    if (rute.AndenPlads != null)
                    {
                        if (rute.AndenPlads.Byder.Andenpladser <= rute.AndenPlads.Byder.MaxAntalVogne)
                        {
                            if (rute.AndenPlads.Byder.LedigeVogne > 0)
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
                    
                }
                
            }
            for (int i = 0; i < Ruter.Count; i++)
            {
                if (Ruter[i].HarVinder == false)
                {
                    if (Ruter[i].AndenPlads != null)
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
        }

        private void TælAndenpladser()
        {
            foreach (var byder in Byderne)
            {
                foreach (var rute in Ruter)
                {
                    if (rute.HarVinder == false)
                    {
                        if (rute.AndenPlads != null)
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
            UpdaterBydereITilbud();
        }

        private void FindVinderFra1Plads()
        {
            TælFørstePladser();
            foreach (var rute in Ruter)
            {
                if (rute.FørstePlads.Byder.Førstepladser <= rute.FørstePlads.Byder.MaxAntalVogne)
                {
                    if (rute.FørstePlads.Byder.LedigeVogne > 0)
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
            UpdaterBydereITilbud();
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
