using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class FoodSpawner
    {
        // Границы зоны, в которой будет появляться еда
        int width, height;
        // Символы еды
        string symbols;
        // Текущая точка еды
        public static Point Food;

        Random rand = new Random();

        public FoodSpawner(int width, int height, string symbols)
        {
            Food = new Point();
            this.width = width;
            this.height = height;
            this.symbols = symbols;
        }

        // Инициализация новых координат и символа еды
        public void Initialize()
        {
            Food.X = rand.Next(1, width - 1);
            Food.Y = rand.Next(1, height - 1);
            Food.Ch = symbols[rand.Next(symbols.Length)];
        }
    }
}
