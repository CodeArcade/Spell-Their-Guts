﻿using Brackeys.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Sprites.Tower
{
    public class WindTower : Tower
    {
        const int BASEDAMAGE = 20;
        const int BASERANGE = 1;
        const float BASEATTACKSPEED = 0.8f;

        public static int GlobalCost { get; set; } = 100;
        public override int Cost => GlobalCost; 

        public WindTower() : base (BASEDAMAGE, BASERANGE, BASEATTACKSPEED, Enemy.Elements.Wind)
        {
            Name = "Wind Mage";
            Texture = ContentManager.TowerTexture;

            AnimationManager.Scale = 3f;
            AnimationManager.Parent = this;
        }

        protected override void ApplyBuff(Tower tower)
        {
            tower.AttackSpeed -= 0.1f;
            if (tower.AttackSpeed < 0.1f) tower.AttackSpeed = 0.1f;
            tower.AttackSpeed = (float)Math.Round(tower.AttackSpeed, 2);
        }

        protected override void RevokeBuff(Tower tower)
        {
            tower.AttackSpeed += 0.1f;
        }

        public override void StartAnimation()
        {
            AnimationManager.Play(new Animation(ContentManager.WindTowerTexture, 4) { FrameSpeed = 0.1f });
        }
    }
}
