using System.Collections.Generic;
using Brackeys.Component.Sprites.Enemy;

namespace Brackeys.Models
{
    public abstract class Stage
    {
        /// <summary>
        /// In seconds
        /// </summary>
        public float SpawnInterval { get; set; }
        public Queue<Enemy> Enemies { get; private set; } = new Queue<Enemy>();

        public bool IsOver => Enemies.Count == 0;

        public void AddEnemy(Enemy enemy, int count)
        {
            for (int i = 0; i < count; i++)
                Enemies.Enqueue(enemy);
        }
    }
}
