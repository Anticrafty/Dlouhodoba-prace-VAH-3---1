using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WandsAndGunsEvolve
{
    public class Domov : Budova
    {

        public Domov()
        {
            obr_odkaz = @"domek.png";
            Potreba_Na_Postaveni = 2;
            potreba_dreva = 50;
            potreba_kamene = 20;
        }

        public override void Crafting()
        {
            throw new NotImplementedException();
        }

        public override void Do()
        {
            if (akce_budovy == "Mnozeni")
            {
                List<Postava> pouzity = new List<Postava>();
                Postava hleda = new Postava();
                foreach (Postava milenec in pracovnici)
                {
                    bool je_pouzity = false;
                    foreach (Postava pouzita_postava in pouzity)
                    {

                        if (milenec == pouzita_postava)
                        {
                            je_pouzity = true;
                        }
                    }


                    if (!je_pouzity)
                    {
                        foreach (Postava milenka in pracovnici)
                        {
                            if (milenka != milenec)
                            {
                                bool je_pouzita = false;
                                foreach (Postava pouzita_postava in pouzity)
                                {
                                    if (milenka == pouzita_postava)
                                    {
                                        je_pouzita = true;
                                    }
                                    if(milenec.muzstvi != milenka.muzstvi)
                                    {
                                        je_pouzita = true;
                                    }
                                }
                                if (!je_pouzita)
                                {
                                    Postava novy = new Bezny_obyvatel { ID = Vesnice.Obyvatele.Count() };
                                    if (Vesnice.rnd_s.Next(1, 3) == 1)
                                    {
                                        novy.muzstvi = true;
                                    }
                                    else
                                    {
                                        novy.muzstvi = false;
                                    }
                                    Vesnice.Obyvatele.Add(novy);
                                    pouzity.Add(milenec);
                                    pouzity.Add(milenka);
                                }
                            }
                        }
                    }
                }
            }
            pracovnici = new List<Postava>();
            akce_budovy = null;
        }

        public override void Uceni()
        {
            throw new NotImplementedException();
        }

        public override void Vyvoj()
        {
            throw new NotImplementedException();
        }
    }
}
