﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class EarthTower : Tower
    {

        const int BASEDAMAGE = 2;
        const int BASERANGE = 3;
        const int BASEATTACKSPEED = 1;
        const int GLOBALCOST = 5;

        public EarthTower() : base(BASEDAMAGE, BASERANGE, BASEATTACKSPEED)
        {
            Name = "Earth Mage";
            Texture = ContentManager.TowerTexture;

            GlobalCost = GLOBALCOST;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.Damage += 1;
            tower.Range += 1;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.Damage -= 1;
            tower.Range -= 1;
        }
    }
}
