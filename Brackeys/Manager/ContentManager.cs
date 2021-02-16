using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Unity;

namespace Brackeys.Manager
{
    public class ContentManager
    {
        [Dependency]
        public JamGame JamGame { get; set; }

        #region Fonts
        public SpriteFont TestFont => JamGame.Content.Load<SpriteFont>("Font/TestFont");
        public SpriteFont KenneyMini(int fontSize = 12)
        {
            switch (fontSize)
            {
                case 14:
                    return JamGame.Content.Load<SpriteFont>("Font/KenneyMini14");
                case 16:
                    return JamGame.Content.Load<SpriteFont>("Font/KenneyMini16");
                case 18:
                    return JamGame.Content.Load<SpriteFont>("Font/KenneyMini18");
                case 12:
                default:
                    return JamGame.Content.Load<SpriteFont>("Font/KenneyMini12");
            }
        }
        #endregion

        public SoundEffect FireShootSoundEffect => JamGame.Content.Load<SoundEffect>("Sounds/FireShoot");
        public SoundEffect WindShootSoundEffect => JamGame.Content.Load<SoundEffect>("Sounds/WindShoot");
        public SoundEffect EarthShootSoundEffect => JamGame.Content.Load<SoundEffect>("Sounds/EarthShoot");
        public SoundEffect SelectSoundEffect => JamGame.Content.Load<SoundEffect>("Sounds/Select");
        public SoundEffect HpLostSoundEffect => JamGame.Content.Load<SoundEffect>("Sounds/HpLost");
        public SoundEffect SpawnSoundEffect => JamGame.Content.Load<SoundEffect>("Sounds/Spawn");

        public Texture2D RangeTexture => JamGame.Content.Load<Texture2D>("Ui/Range");
        public Texture2D TowerTexture => JamGame.Content.Load<Texture2D>("Sprites/Towers/TEST/Tower");
        public Texture2D Tower2Texture => JamGame.Content.Load<Texture2D>("Sprites/Towers/TEST/Tower2");
        public Texture2D EnemyTexture => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Enemy");
        public Texture2D TransparentTexture => JamGame.Content.Load<Texture2D>("Sprites/Transparent");

        public List<Texture2D> ObstacleHitParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle1"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle2"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle3")
            };
        }

        public List<Texture2D> EntityHitParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle1"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle2"),
                JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle3")
            };
        }

        public List<Texture2D> EntityDeathParticle
        {
            get => new List<Texture2D>()
            {
               JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle1"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle2"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/ObstacleHitParticle3"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle1"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle2"),
               JamGame.Content.Load<Texture2D>("Sprites/Particle/EntityHitParticle3")
            };
        }


    }
}