//using Brackeys.Component.Sprites.Enemies;
//using Brackeys.Component.Sprites.Environment;
using Brackeys.Component.Sprites.Enemy;
using Brackeys.Component.Sprites.Tower;
using Brackeys.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace Brackeys.Component.Sprites
{
    public class Projectile : Sprite
    {
        private double TTL { get; set; }
        private double TimeLived { get; set; }
        private Elements Element { get; set; }
        private int Damage { get; set; }

        public Projectile(int damage, Vector2 direction, Tower.Tower parent, Elements element, Texture2D texture = null, Size? size = null, Animation animation = null)
        {
            Parent = parent;
            Position = parent.Center;
            Speed = 500;
            Direction = direction;
            Damage = damage;

            if (texture is null)
                Texture = ContentManager.TowerTexture;
            else
                Texture = texture;

            if (animation != null)
            {
                AnimationManager.Parent = this;
                AnimationManager.Play(animation);
            }

            if (size is null)
                Size = new Size(10, 10);
            else
                Size = (Size)size;

            TTL = parent.Range * 1000;
            Element = element;

            Direction = new Vector2(Direction.X, Direction.Y);
        }

        public override void Update(GameTime gameTime)
        {
            TimeLived += gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeLived >= TTL)
            {
                IsRemoved = true;
                return;
            }
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void OnCollision(Sprite sprite, GameTime gameTime)
        {
            if (sprite == Parent) return;
            if (sprite == this) return;
            if (sprite is Projectile) return;
            if (IsRemoved) return;

            if (!(sprite is Enemy.Enemy)) return;

            ((Enemy.Enemy)sprite).TakeDamage(Damage, Element);
            ((Enemy.Enemy)sprite).VirtualHealth = ((Enemy.Enemy)sprite).Health;

            if (((Enemy.Enemy)sprite).Health <= 0) sprite.IsRemoved = true;
            IsRemoved = true;
        }
    }
}