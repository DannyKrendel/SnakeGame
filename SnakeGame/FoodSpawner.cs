using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class FoodSpawner : ColorObject
    {
        Point p1;
        Point p2;
        char foodCh;

        Random rand = new Random();

        public FoodSpawner(Point p1, Point p2, char ch, ConsoleColor bgColor, ConsoleColor fgColor) : base(bgColor, fgColor)
        {
            this.p1 = Point.Clone(p1);
            this.p2 = Point.Clone(p2);
            foodCh = ch;
        }

        public Point Spawn()
        {
            int x = rand.Next(p1.X + 1, p2.X - 1);
            int y = rand.Next(p1.Y + 1, p2.Y - 1);
            return new Point(x, y, foodCh, bgColor, fgColor);
        }
    }
}
