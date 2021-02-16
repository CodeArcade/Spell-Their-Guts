using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class FireTower : Tower
    {
        const int BASEDAMAGE = 3;
        const int BASERANGE = 2;
        const int BASEATTACKSPEED = 1;
        const int GLOBALCOST = 5;

        public FireTower() : base(BASEDAMAGE, BASERANGE, BASEATTACKSPEED, Enemy.Elements.Fire)
        {
            Name = "Fire Mage";
            GlobalCost = GLOBALCOST;
            Texture = ContentManager.TowerTexture;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.Damage += 2;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.Damage -= 2;
        }
    }
}
