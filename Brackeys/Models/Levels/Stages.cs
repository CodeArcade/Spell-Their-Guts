using Brackeys.Component.Sprites.Enemy;
using System.Collections.Generic;

namespace Brackeys.Models.Levels
{
    public class Stages
    {
        public List<Stage> StageList { get; set; }

        public Stages()
        {
            StageList = new List<Stage>()
            {
                Stage1(),
                Stage2(),
                Stage3(),
                Stage4(),
                Stage5(),
                Stage6(),
                Stage7(),
                Stage8(),
                Stage9(),
                Stage10()
            };
        }

        private Stage Stage1()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 2
            };

            stage.AddEnemy(new Walker(Elements.Earth), 10);

            return stage;
        }

        private Stage Stage2()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 2
            };

            stage.AddEnemy(new Walker(Elements.Fire), 10);

            return stage;
        }

        private Stage Stage3()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 2
            };

            stage.AddEnemy(new Walker(Elements.Wind), 10);

            return stage;
        }

        private Stage Stage4()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 2
            };

            stage.AddEnemy(new Tank(Elements.Earth), 15);

            return stage;
        }

        private Stage Stage5()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 2
            };

            stage.AddEnemy(new Tank(Elements.Fire), 15);

            return stage;
        }

        private Stage Stage6()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 2
            };

            stage.AddEnemy(new Tank(Elements.Wind), 15);

            return stage;
        }

        private Stage Stage7()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 1
            };

            stage.AddEnemy(new Runner(Elements.Earth), 20);

            return stage;
        }

        private Stage Stage8()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 1
            };

            stage.AddEnemy(new Runner(Elements.Fire), 20);

            return stage;
        }

        private Stage Stage9()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 1
            };

            stage.AddEnemy(new Runner(Elements.Wind), 20);

            return stage;
        }

        private Stage Stage10()
        {
            Stage stage = new Stage
            {
                SpawnInterval = 1
            };

            bool spawnOne = false;

            for (int i = 0; i < 30; i++)
            {
                if (spawnOne)
                    stage.AddEnemy(new Walker(Elements.Earth), 1);
                else
                    stage.AddEnemy(new Walker(Elements.Fire), 1);

                spawnOne = !spawnOne;
            }

            return stage;
        }
    }
}
