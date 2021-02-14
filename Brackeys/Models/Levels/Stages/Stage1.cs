using System;
using System.Collections.Generic;
using System.Text;
using Brackeys.Component.Sprites.Enemy;

namespace Brackeys.Models.Levels.Stages
{
    public class Stage1 : Stage
    {
        public Stage1()
        {
            SpawnInterval = 2;

            AddEnemy(new Enemy(), 10);
        }
    }
}
