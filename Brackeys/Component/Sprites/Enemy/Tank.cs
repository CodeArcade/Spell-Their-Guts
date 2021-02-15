using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Enemy
{
    public class Tank : Enemy
    {
        public Tank(Elements element): base(element)
        {
            Texture = ContentManager.EnemyTexture;
            Speed = 50;
            Damage = 1;
            Health = 10;
        }
    }
}
