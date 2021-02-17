using Brackeys.Models;
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
            Animations = new Dictionary<string, Animation>
            {
                {"walk", new Animation(ContentManager.KnightTexture, 4) }
            };
            AnimationManager.Play(Animations["walk"]);
            AnimationManager.Parent = this;

            Speed = 50;
            Damage = 1;
            Health = 10;
            VirtualHealth = 10;
            Reward = 3;
        }
    }
}
