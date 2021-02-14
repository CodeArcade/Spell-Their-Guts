using Microsoft.Xna.Framework;

namespace Brackeys.Models.Levels
{
    public class Level1 : Level
    {
        public Level1()
        {
            SpawnPoint = new Point(0, 0);
            Target = new Point(1, 11);

            Paths.Add(new Path(0, 8, 0, 0));
            Paths.Add(new Path(0, 8, 5, 8));
            Paths.Add(new Path(5, 5, 5, 8));
            Paths.Add(new Path(5, 5, 15, 5));
            Paths.Add(new Path(15, 5, 15, 15));
            Paths.Add(new Path(1, 15, 15, 15));
            Paths.Add(new Path(1, 11, 1, 15));
        }
    }
}
