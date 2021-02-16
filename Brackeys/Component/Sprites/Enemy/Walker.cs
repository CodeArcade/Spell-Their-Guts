using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Enemy
{
    public class Walker: Enemy
    {
        public Walker(Elements element) : base(element)
        {
            Texture = ContentManager.EnemyTexture;
            Speed = 700;
            Damage = 10;
            Health = 6;
            VirtualHealth = 6;
            Reward = 1;
        }
    }
}
