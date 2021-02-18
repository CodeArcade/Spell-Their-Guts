using Microsoft.Xna.Framework;

namespace Brackeys.Models.Levels
{
    public class Level1 : Level
    {
        public Level1()
        {
            SpawnPoint = new Point(4, 0);
            Target = new Point(10, 15);

            Paths.Add(new Path(4, 4, 4, 0));
            Paths.Add(new Path(3, 4, 2, 4));
            Paths.Add(new Path(2, 5, 2, 11));
            Paths.Add(new Path(3, 11, 4, 11));
            Paths.Add(new Path(5, 8, 5, 11));
            Paths.Add(new Path(6, 8, 13, 8));
            Paths.Add(new Path(13, 5, 13, 7));
            Paths.Add(new Path(8, 5, 12, 5));
            Paths.Add(new Path(8, 4, 8, 2));
            Paths.Add(new Path(9, 2, 17, 2));
            Paths.Add(new Path(17, 3, 17, 12));
            Paths.Add(new Path(10, 12, 16, 12));
            Paths.Add(new Path(10, 13, 10, 15));
        }
    }
}
