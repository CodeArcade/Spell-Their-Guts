using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class WindTower : Tower
    {
        public WindTower()
        {
            Name = "Wind Mage";
            Texture = ContentManager.TowerTexture;

            BaseDamage = 2;
            BaseRange = 2;
            BaseAttackSpeed = 1;
            GlobalCost = 1;
        }
    }
}
