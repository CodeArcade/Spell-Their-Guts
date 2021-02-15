using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    class NormalTower : Tower
    {

        const int BASEDAMAGE = 1;
        const int BASERANGE = 2;
        const int BASEATTACKSPEED = 1;
        const int GLOBALCOST = 1;
        public NormalTower() : base(BASEDAMAGE, BASERANGE, BASEATTACKSPEED)
        {
            Name = "Mage";
            GlobalCost = GLOBALCOST;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.Damage += 1;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.Damage -= 1;
        }
    }
}
