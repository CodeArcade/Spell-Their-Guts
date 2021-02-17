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
            Speed = 70;
            Damage = 1;
            Health = 6;
            VirtualHealth = 6;
            Reward = 1;
        }

        public override void LevelUp()
        {
            Health += 2;
            VirtualHealth += 2;
        }
    }
}
