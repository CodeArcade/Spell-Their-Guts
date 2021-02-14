using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Brackeys.Models
{
    /// <summary>
    /// only lines in cardinal directions please
    /// https://stackoverflow.com/questions/11678693/all-cases-covered-bresenhams-line-algorithm
    /// </summary>
    public class Path
    {
        public Point A { get; set; }
        public Point B { get; set; }

        private List<Point> Points { get; set; }
        public List<Point> Track => Points;

        public Path(int xA, int yA, int xB, int yB)
        {
            A = new Point(xA, yA);
            B = new Point(xB, yB);
            Points = Bresenham(A, B);
        }


        private List<Point> Bresenham(Point p1, Point p2)
        {
            List<Point> track = new List<Point>();
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;

            int swaps = 0;
            if (dy > dx)
            {
                Swap(ref dx, ref dy);
                swaps = 1;
            }

            int a = Math.Abs(dy);
            int b = -Math.Abs(dx);

            double d = 2 * a + b;
            int x = p1.X;
            int y = p1.Y;
            track.Clear();
            track.Add(new Point(x, y));

            int s = 1;
            int q = 1;
            if (p1.X > p2.X) q = -1;
            if (p1.Y > p2.Y) s = -1;

            for (int k = 0; k < dx; k++)
            {
                if (d >= 0)
                {
                    d = 2 * (a + b) + d;
                    y += s;
                    x += q;
                }
                else
                {
                    if (swaps == 1) y += s;
                    else x += q;
                    d = 2 * a + d;
                }
                track.Add(new Point(x, y));
            }

            return track;
        }

        private void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

    }
}