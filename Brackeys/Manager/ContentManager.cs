﻿using Microsoft.Xna.Framework.Audio;
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
            return fontSize switch
            {
                14 => JamGame.Content.Load<SpriteFont>("Font/KenneyMini14"),
                16 => JamGame.Content.Load<SpriteFont>("Font/KenneyMini16"),
                18 => JamGame.Content.Load<SpriteFont>("Font/KenneyMini18"),
                _ => JamGame.Content.Load<SpriteFont>("Font/KenneyMini12"),
            };
        }
        #endregion

        public Texture2D TutorialFairy => JamGame.Content.Load<Texture2D>("Sprites/Tutorial/Fairy");

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

        public Texture2D FireTowerTexture => JamGame.Content.Load<Texture2D>("Sprites/Towers/FireMage");
        public Texture2D EarthTowerTexture => JamGame.Content.Load<Texture2D>("Sprites/Towers/StoneMage");
        public Texture2D WindTowerTexture => JamGame.Content.Load<Texture2D>("Sprites/Towers/WindMage");
        public Texture2D NormalTowerTexture => JamGame.Content.Load<Texture2D>("Sprites/Towers/NormalMage");

        public Texture2D BlueProjectileTexture => JamGame.Content.Load<Texture2D>("Sprites/Projectiles/BlueProjectile");
        public Texture2D RedProjectileTexture => JamGame.Content.Load<Texture2D>("Sprites/Projectiles/RedProjectile");
        public Texture2D StoneProjectileTexture => JamGame.Content.Load<Texture2D>("Sprites/Projectiles/StoneProjectile");

        public Texture2D SkullTexture => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Skull");
        public Texture2D KnightTexture => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Knight");
        public Texture2D RockTexture => JamGame.Content.Load<Texture2D>("Sprites/Enemy/Rock");
        public Texture2D FireEnemyTexture => JamGame.Content.Load<Texture2D>("Sprites/Enemy/FireThing");

        public Texture2D BlueProjectile => JamGame.Content.Load<Texture2D>("Sprites/Projectiles/BlueProjectile");
        public Texture2D RedProjectile => JamGame.Content.Load<Texture2D>("Sprites/Projectiles/RedProjectile");
        public Texture2D StoneProjectile => JamGame.Content.Load<Texture2D>("Sprites/Projectiles/StoneProjectile");

        public Texture2D MouseCursor => JamGame.Content.Load<Texture2D>("Sprites/GUI/Cursor");
        public Texture2D Title => JamGame.Content.Load<Texture2D>("Sprites/GUI/Title");
        public Texture2D BlueButton => JamGame.Content.Load<Texture2D>("Sprites/GUI/BlueButton");
        public Texture2D GreyButton => JamGame.Content.Load<Texture2D>("Sprites/GUI/GreyButton");
        public Texture2D GreenButton => JamGame.Content.Load<Texture2D>("Sprites/GUI/GreenButton");
        public Texture2D WhiteButton => JamGame.Content.Load<Texture2D>("Sprites/GUI/WhiteButton");
        public Texture2D SmallWhiteButton => JamGame.Content.Load<Texture2D>("Sprites/GUI/SmallWhiteButton");
        public Texture2D Textbox => JamGame.Content.Load<Texture2D>("Sprites/GUI/Textbox");
        public Texture2D TextboxAdvance => JamGame.Content.Load<Texture2D>("Sprites/GUI/TextAdvance");
        public Texture2D MenuBackground => JamGame.Content.Load<Texture2D>("Sprites/Background/Title");
        public Texture2D ShopBackground => JamGame.Content.Load<Texture2D>("Sprites/GUI/ShopBackground");

        public Texture2D GrassTile => JamGame.Content.Load<Texture2D>("Sprites/Background/Grass");

        public List<Texture2D> MainFireTowerParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Particles/MainTower/Fire/Particle1"),
            };
        }

        public List<Texture2D> MainEarthTowerParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Particles/MainTower/Earth/Particle1"),
            };
        }

        public List<Texture2D> MainWindTowerParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Particles/MainTower/Wind/Particle1"),
            };
        }

        public List<Texture2D> SupportFireTowerParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Particles/SupportTower/Fire/Particle1"),
            };
        }

        public List<Texture2D> SupportEarthTowerParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Particles/SupportTower/Earth/Particle1"),
            };
        }

        public List<Texture2D> SupportWindTowerParticle
        {
            get => new List<Texture2D>()
            {
                JamGame.Content.Load<Texture2D>("Particles/SupportTower/Wind/Particle1"),
            };
        }
    }
}