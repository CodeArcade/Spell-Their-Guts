using Brackeys.Component.Sprites.Enemy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Models.Levels.Stages
{
    public class Stage2: Stage
    {
        public Stage2()
        {
            SpawnInterval = 1;

            AddEnemy(new Enemy(), 5);
        }
    }
}
