using Brackeys.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    class NormalTower : Tower
    {
        const int BASEDAMAGE = 1;
        const int BASERANGE = 2;
        const int BASEATTACKSPEED = 1;

        public static int GlobalCost { get; set; } = 100;
        public override int Cost => GlobalCost;

        public NormalTower() : base(BASEDAMAGE, BASERANGE, BASEATTACKSPEED, Enemy.Elements.None)
        {
            Name = "Mage";

            AnimationManager.Scale = 3f;
            AnimationManager.Parent = this;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.Damage += 1;
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.Damage -= 1;
        }

        public override void StartAnimation()
        {
            AnimationManager.Play(new Animation(ContentManager.NormalTowerTexture, 4) { FrameSpeed = 0.1f });
        }
    }
}
