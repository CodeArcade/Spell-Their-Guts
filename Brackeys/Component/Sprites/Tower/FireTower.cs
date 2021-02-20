using Brackeys.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class FireTower : Tower
    {
        const int BASEDAMAGE = 40;
        const int BASERANGE = 1;
        const float BASEATTACKSPEED = 1.5f;

        public static int GlobalCost { get; set; } = 100;
        public override int Cost => GlobalCost;

        public FireTower() : base(BASEDAMAGE, BASERANGE, BASEATTACKSPEED, Enemy.Elements.Fire)
        {
            Name = "Fire Mage";
            Texture = ContentManager.TowerTexture;

            AnimationManager.Scale = 3f;
            AnimationManager.Parent = this;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.Damage += 10;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.Damage -= 10;
        }

        public override void StartAnimation()
        {
            AnimationManager.Play(new Animation(ContentManager.FireTowerTexture, 4) { FrameSpeed = 0.1f });
        }
    }
}
