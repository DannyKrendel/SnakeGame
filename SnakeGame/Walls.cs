using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Walls : IColor
    {
        public int Width { get; set; }
        public int Height { get; set; }

        char wallCh;
        List<Figure> wallList;

        public Walls(int fieldWidth, int fieldHeight, char wallCh)
        {
            Width = fieldWidth;
            Height = fieldHeight;

            wallList = new List<Figure>
            {
                new Line(0,          0,           fieldWidth,  wallCh, Direction.Right),
                new Line(fieldWidth, 0,           fieldHeight, wallCh, Direction.Down),
                new Line(fieldWidth, fieldHeight, fieldWidth,  wallCh, Direction.Left),
                new Line(0,          fieldHeight, fieldHeight, wallCh, Direction.Up)
            };

            this.wallCh = wallCh;
        }

        public void Draw(ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            foreach (var wall in wallList)
            {
                wall.Draw(bgColor, fgColor);
            }
        }
    }
}
