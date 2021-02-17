using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Brackeys.States;
using Brackeys.Component.Sprites.Enemy;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;
using Brackeys.Models.Levels;
using Brackeys.Manager;

namespace Brackeys.Models
{
    public abstract class Level
    {
        public List<Path> Paths { get; private set; } = new List<Path>();
        public Point SpawnPoint { get; protected set; }
        public Point Target { get; protected set; }

        public Queue<Stage> Stages { get; private set; }
        public int CurrentStage { get; private set; }

        public float TimeSinceLastSpawn { get; set; }
        private bool IsRunning { get; set; }

        public Level()
        {
            Stages = new Queue<Stage>();

            foreach (Stage stage in new Stages().StageList)
                Stages.Enqueue(stage);
        }

        public void Start()
        {
            if (CurrentStage == 0) CurrentStage++;

            IsRunning = true;
        }

        public void Pause()
        {
            IsRunning = false;
        }

        private void StartNextStage()
        {
            TimeSinceLastSpawn = 0;
            CurrentStage++;
            Stages.Dequeue();

        }

        public void Update(GameTime gameTime, GameState gameState)
        {
            if (!IsRunning) return;

            if (Stages.Count == 0)
            {
                IsRunning = false;
                return;
            }

            TimeSinceLastSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeSinceLastSpawn >= Stages.First().SpawnInterval)
            {
                TimeSinceLastSpawn = 0;
                SpawnEnemey(gameState);
            }

            if (Stages.First().IsOver && !gameState.Layers[(int)Layers.PlayingArea].Any(x => x is Enemy))
            {
                StartNextStage();
            }
        }

        private void SpawnEnemey(GameState gameState)
        {
            if (Stages.First().Enemies.Count == 0) return;

            foreach (Enemy e in Stages.First().Enemies.Dequeue())
            {
                Enemy enemy = (Enemy)e.Copy();
                for (int i = 0; i < CurrentStage; i++)
                    enemy.LevelUp();

                enemy.Position = SpawnPoint.ToVector2() * gameState.CellSize;
                enemy.Coordinate = SpawnPoint;
                enemy.Size = new Size(gameState.CellSize, gameState.CellSize);
                enemy.TargetCoordinate = Target;
                gameState.AddComponent(enemy, (int)Layers.PlayingArea);
            }

            new AudioManager().PlayEffect(gameState.ContentManager.SpawnSoundEffect, -0.25f);
        }
    }
}
