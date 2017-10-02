using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Walls : ColorObject
    {
        List<Figure> wallList;
        char wallCh;

        public Walls(int fieldWidth, int fieldHeight, char wallCh, ConsoleColor bgColor, ConsoleColor fgColor) : base(bgColor, fgColor)
        {
            wallList = new List<Figure>();
            this.wallCh = wallCh;

            Line line1 = new Line(0, 0, fieldWidth, wallCh, bgColor, fgColor, Direction.Right);
            Line line2 = new Line(fieldWidth, 0, fieldHeight, wallCh, bgColor, fgColor, Direction.Down);
            Line line3 = new Line(fieldWidth, fieldHeight, fieldWidth, wallCh, bgColor, fgColor, Direction.Left);
            Line line4 = new Line(0, fieldHeight, fieldHeight, wallCh, bgColor, fgColor, Direction.Up);

            wallList.Add(line1);
            wallList.Add(line2);
            wallList.Add(line3);
            wallList.Add(line4);
        }

        public bool IsHit(Figure figure)
        {
            foreach (var wall in wallList)
            {
                if (wall.IsHit(figure))
                    return true;
            }
            return false;
        }

        public void Draw()
        {
            foreach (var wall in wallList)
            {
                wall.Draw();
            }
        }
    }
}
