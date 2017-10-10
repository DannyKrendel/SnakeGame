using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Walls : Colored
    {
        // Длина и высота стен
        public int Width { get; set; }
        public int Height { get; set; }

        // Символ стен
        char wallCh;

        List<Figure> wallList;

        public Walls(int fieldWidth, int fieldHeight, char wallCh)
        {
            Width = fieldWidth;
            Height = fieldHeight;
            this.wallCh = wallCh;

            // Создание стен из четырех линий
            wallList = new List<Figure>
            {
                new Line(0,     0,      Width,  wallCh, Direction.Right),
                new Line(Width, 0,      Height, wallCh, Direction.Down),
                new Line(Width, Height, Width,  wallCh, Direction.Left),
                new Line(0,     Height, Height, wallCh, Direction.Up)
            };
        }

        // Рисовка стен
        public void Draw()
        {
            foreach (var wall in wallList)
            {
                wall.SetColor(bgColor, fgColor);
                wall.Draw();
            }
        }
    }
}
