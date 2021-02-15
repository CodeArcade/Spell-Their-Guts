using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class EarthTower : Tower
    {
        public EarthTower()
        {
            Name = "Earth Mage";
            Texture = ContentManager.TowerTexture;

            BaseDamage = 2;
            BaseRange = 2;
            BaseAttackSpeed = 1;
            GlobalCost = 1;
        }
    }
}
