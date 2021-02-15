using System.Collections.Generic;
using Brackeys.Component.Sprites.Enemy;

namespace Brackeys.Models
{
    public class Stage
    {
        /// <summary>
        /// In seconds
        /// </summary>
        public float SpawnInterval { get; set; }
        public Queue<List<Enemy>> Enemies { get; private set; } = new Queue<List<Enemy>>();

        public bool IsOver => Enemies.Count == 0;

        public void AddEnemy(Enemy enemy, int count)
        {
            for (int i = 0; i < count; i++)
                Enemies.Enqueue(new List<Enemy>() { enemy });
        }

        public void AddEnemy(List<Enemy> enemies, int count)
        {
            for (int i = 0; i < count; i++)
                Enemies.Enqueue(enemies);
        }
    }
}
