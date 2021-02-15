using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Brackeys.Models.Levels.Stages;
using Brackeys.States;
using Brackeys.Component.Sprites.Enemy;
using System.Drawing;
using Point = Microsoft.Xna.Framework.Point;

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
            Stages.Enqueue(new Stage1());
            Stages.Enqueue(new Stage2());
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
            CurrentStage ++;
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

            if (Stages.First().IsOver)
            {
                StartNextStage();
            }
        }

        private void SpawnEnemey(GameState gameState)
        {
            if (Stages.First().Enemies.Count == 0) return;
            Enemy enemy = (Enemy)Stages.First().Enemies.Dequeue().Copy();
            enemy.Position = SpawnPoint.ToVector2() * gameState.CellSize;
            enemy.Coordinate = SpawnPoint;
            enemy.Size = new Size(gameState.CellSize, gameState.CellSize);
            enemy.TargetCoordinate = Target;
            gameState.AddComponent(enemy, (int)Layers.PlayingArea);
        }
    }
}
