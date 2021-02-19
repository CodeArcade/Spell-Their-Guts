using Brackeys.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class EarthTower : Tower
    {
        const int BASEDAMAGE = 30;
        const int BASERANGE = 1;
        const int BASEATTACKSPEED = 1;

        public static int GlobalCost { get; set; } = 100;
        public override int Cost => GlobalCost;

        public EarthTower() : base(BASEDAMAGE, BASERANGE, BASEATTACKSPEED, Enemy.Elements.Earth)
        {
            Name = "Earth Mage";
            Texture = ContentManager.TowerTexture;

            AnimationManager.Scale = 3f;
            AnimationManager.Parent = this;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.Range += 0.6f;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.Range -= 0.6f;
        }

        public override void StartAnimation()
        {
            AnimationManager.Play(new Animation(ContentManager.EarthTowerTexture, 4) { FrameSpeed = 0.1f });
        }
    }
}
