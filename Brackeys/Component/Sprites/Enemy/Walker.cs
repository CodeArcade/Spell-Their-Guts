using Brackeys.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            HitboxSize = new Size((int)(Size.Width * 0.9), (int)(Size.Height * 0.9));
            HitBoxXOffSet = (int)((Size.Width * 0.9) / 2);
            HitBoxYOffSet = (int)((Size.Height * 0.9 / 2));

            Speed = 70;
            Damage = 10;
            Health = 60;
            VirtualHealth = 60;
            Reward = 10;
        }

        public override void LevelUp()
        {
            Health += 5;
            VirtualHealth += 5;
        }
    }
}
