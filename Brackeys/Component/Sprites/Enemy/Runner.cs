using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Enemy
{
    public class Runner: Enemy
    {
        public Runner(Elements element): base(element)
        {
            Texture = ContentManager.EnemyTexture;
            Speed = 100;
            Damage = 1;
            Health = 3;
            VirtualHealth = 3;
            Reward = 2;
        }

        public override void LevelUp()
        {
            Speed += 10;
            Health += 1;
            VirtualHealth += 3;
        }
    }
}
