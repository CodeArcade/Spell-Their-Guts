//using Brackeys.Component.Effects;
//using Brackeys.Component.Sprites.Enemies;
//using Brackeys.Component.Sprites.Environment;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace Brackeys.Component.Sprites
{

    public class Entity : Sprite
    {
        public float Health { get; set; }
        public int Damage { get; set; }
       
        //protected virtual Texture2D SpotShadow => ContentManager.SpotShadowTexture; 

        public override void Update(GameTime gameTime)
        {
            CheckHealth();
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(SpotShadow, Position, Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        protected void CheckHealth()
        {
            if (Health > 0) return;

            //ParticleManager.GenerateNewParticle(Color.White, Position, ContentManager.EntityDeathParticle, 5, 10);
            //AudioManager.PlayEffect(ContentManager.EntityDeathSoundEffect, 0.25f);
            IsRemoved = true;
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}