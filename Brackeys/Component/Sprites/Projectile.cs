//using Brackeys.Component.Sprites.Enemies;
//using Brackeys.Component.Sprites.Environment;
using Brackeys.Component.Sprites.Enemy;
using Brackeys.Component.Sprites.Tower;
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

        public Projectile(Vector2 direction, Tower.Tower parent, Elements element, Texture2D texture = null, Size? size = null)
        {
            Parent = parent;
            Position = parent.Position;
            Speed = 500;
            Direction = direction;

            //if (texture is null)
            //    Texture = ContentManager.PlayerBulletTexture;
            //else
            //    Texture = texture;

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

            // ParticleManager.GenerateNewParticle(Color.White, Position, ContentManager.EntityHitParticle, 5, 10);
            //AudioManager.PlayEffect(ContentManager.EntityHitSoundEffect, 0.25f);

            ((Enemy.Enemy)sprite).TakeDamage(((Tower.Tower)Parent).Damage, Element);
            IsRemoved = true;
        }

    }
}