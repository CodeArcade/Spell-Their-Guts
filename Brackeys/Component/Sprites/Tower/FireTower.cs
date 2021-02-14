using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class FireTower : Tower
    {
        public FireTower()
        {
            Name = "Fire Wizard";
            Texture = ContentManager.TowerTexture;

            Damage = 2;
            Range = 5;
            Cost = 10;
        }
    }
}
