using Brackeys.Models;
using Microsoft.Xna.Framework;
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
            Animations = new Dictionary<string, Animation>
            {
                {"walk", new Animation(ContentManager.RockTexture, 4) { FrameSpeed = 0.1f } }
            };
            AnimationManager.Scale = 3;
            AnimationManager.Parent = this;
            AnimationManager.Play(Animations["walk"]);

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
