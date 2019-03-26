using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandsAndGunsEvolve.Classy.Postavy;

namespace WandsAndGunsEvolve
{
    class Dilna : Budova
    {
        public Dilna()
        {
            obr_odkaz = @"Dilna.png";
            Potreba_Na_Postaveni = 5;
            Crafting_exist = true;
            Uceni_exist = true;
            Vyvoj_exist = true;
            potreba_dreva = 50;
            potreba_kamene = 100;
        }

        public override void Crafting()
        {
            foreach(Postava makeri in pracovnici)
            {
                Vesnice.items.Add(craft_ceho);
            }
        }

        public override void Do()
        {
            if (akce_budovy == "Craft")
            {
                Crafting();
            }
            else if (akce_budovy == "Vyvoj")
            {
                Vyvoj();
            }
            else if (akce_budovy == "Uceni")
            {
                Uceni();
            }
        }

        public override void Uceni()
        {
            if (craft_ceho == "Stavitel")
            {
                foreach(Postava student in pracovnici)
                {
                    int id_studenta = 0;
                    int id_controlovanych = 0;
                    foreach(Postava obyvatel in Vesnice.Obyvatele)
                    {
                        if (student == obyvatel)
                        {
                            id_studenta = id_controlovanych;
                        }
                        id_controlovanych++;
                    }
                    Vesnice.Obyvatele[id_studenta] = new Stavitel() { ID = id_studenta, muzstvi = Vesnice.Obyvatele[id_studenta].muzstvi, vek = Vesnice.Obyvatele[id_studenta].vek };
                }
            }
        }

        public override void Vyvoj()
        {
            Vesnice.vynalezy.Add(craft_ceho);
        }
    }
}
