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

        public WindTower() : base (BASEDAMAGE, BASERANGE, BASEATTACKSPEED, Enemy.Elements.Wind)
        {
            Name = "Wind Mage";
            Texture = ContentManager.TowerTexture;
            GlobalCost = GLOBALCOST;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.AttackSpeed -= 0.1f;
            if (tower.AttackSpeed < 0.1f) tower.AttackSpeed = 0.1f;
            tower.AttackSpeed = (float)Math.Round(tower.AttackSpeed, 2);
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.AttackSpeed += 0.1f;
        }
    }
}
