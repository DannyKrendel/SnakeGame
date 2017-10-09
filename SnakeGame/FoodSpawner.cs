using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class FoodSpawner
    {
        int width, height;
        char ch;
        public static Point Food;

        Random rand = new Random();

        public FoodSpawner(int width, int height, char ch)
        {
            this.width = width;
            this.height = height;
            this.ch = ch;
        }

        public void Initialize()
        {
            int x = rand.Next(1, width - 1);
            int y = rand.Next(1, height - 1);
            Food = new Point(x, y, ch);
        }
    }
}
