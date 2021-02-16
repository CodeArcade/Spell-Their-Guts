﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class EarthTower : Tower
    {

        const int BASEDAMAGE = 2;
        const int BASERANGE = 2;
        const int BASEATTACKSPEED = 1;
        const int GLOBALCOST = 5;

        public EarthTower() : base(BASEDAMAGE, BASERANGE, BASEATTACKSPEED, Enemy.Elements.Earth)
        {
            Name = "Earth Mage";
            Texture = ContentManager.TowerTexture;

            GlobalCost = GLOBALCOST;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.Range += 0.5f;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.Range -= 0.5f;
        }
    }
}
