using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class WaterTower : Tower
    {
        public WaterTower()
        {
            Name = "Water Mage";
            Texture = ContentManager.TowerTexture;

            BaseDamage = 2;
            BaseRange = 2;
            BaseAttackSpeed = 1;
            GlobalCost = 1;
        }
    }
}
