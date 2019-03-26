using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public override void Uceni()
        {
            throw new NotImplementedException();
        }

        public override void Vyvoj()
        {
            Vesnice.vynalezy.Add(craft_ceho);
        }
    }
}
