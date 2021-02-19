using Brackeys.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                {"walk", new Animation(ContentManager.KnightTexture, 4) { FrameSpeed = 0.1f} }
            };
            AnimationManager.Parent = this;
            AnimationManager.Scale = 3;
            AnimationManager.Play(Animations["walk"]);

            HitboxSize = new Size((int)(Size.Width * 0.9), (int)(Size.Height * 0.9));
            HitBoxXOffSet = (int)((Size.Width * 0.9) / 2);
            HitBoxYOffSet = (int)((Size.Height * 0.9 / 2));

            Speed = 50;
            Damage = 10;
            Health = 100;
            VirtualHealth = 100;
            Reward = 10;
        }

        public override void LevelUp()
        {
            Health += 10;
            VirtualHealth += 10;
        }
    }
}
