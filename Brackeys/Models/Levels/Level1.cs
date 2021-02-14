using Microsoft.Xna.Framework;

namespace Brackeys.Models.Levels
{
    public class Level1 : Level
    {
        public Level1()
        {
            SpawnPoint = new Point(0, 0);
            Target = new Point(5, 8);

            Paths.Add(new Path(0, 0, 0, 8));
            Paths.Add(new Path(1, 8, 5, 8));
            Paths.Add(new Path(5, 5, 5, 7));
            //Paths.Add(new Path(3, 5, 4, 5));
            //Paths.Add(new Path(3, 6, 3, 12));
        }
    }
}
