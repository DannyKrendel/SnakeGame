using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Line : Figure
    {
        char lineCh;
        Direction direction;

        public Line(int x, int y, int length, char ch, ConsoleColor bgColor, ConsoleColor fgColor, Direction dir) : base (bgColor, fgColor)
        {
            pList = new List<Point>();
            lineCh = ch;
            direction = dir;

            switch (direction)
            {
                case Direction.Left:
                    for (int i = x; i > x - length; i--)
                    {
                        Point p = new Point(i, y, lineCh, bgColor, fgColor);
                        pList.Add(p);
                    }
                    break;
                case Direction.Right:
                    for (int i = x; i < x + length; i++)
                    {
                        Point p = new Point(i, y, lineCh, bgColor, fgColor);
                        pList.Add(p);
                    }
                    break;
                case Direction.Up:
                    for (int i = y; i > y - length; i--)
                    {
                        Point p = new Point(x, i, lineCh, bgColor, fgColor);
                        pList.Add(p);
                    }
                    break;
                case Direction.Down:
                    for (int i = y; i < y + length; i++)
                    {
                        Point p = new Point(x, i, lineCh, bgColor, fgColor);
                        pList.Add(p);
                    }
                    break;
            }
        }
    }
}
