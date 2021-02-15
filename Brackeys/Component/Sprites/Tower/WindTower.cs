using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class WindTower : Tower
    {
        const int BASEDAMAGE = 2;
        const int BASERANGE = 2;
        const int BASEATTACKSPEED = 1;
        const int GLOBALCOST = 5;

        public WindTower() : base (BASEDAMAGE, BASERANGE, BASEATTACKSPEED)
        {
            Name = "Wind Mage";
            Texture = ContentManager.TowerTexture;
            GlobalCost = GLOBALCOST;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.AttackSpeed += 2;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.AttackSpeed -= 2;
        }
    }
}
