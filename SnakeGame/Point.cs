using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Point : ColorObject
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal char Ch { get; set; }

        public Point(int x, int y, char ch, ConsoleColor bgColor, ConsoleColor fgColor) : base(bgColor, fgColor)
        {
            this.X = x;
            this.Y = y;
            this.Ch = ch;
        }

        public void Draw()
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.SetCursorPosition(X, Y);
            Console.Write(Ch);
            Console.ResetColor();
        }

        public void Move(int offset, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    X += offset;
                    break;
                case Direction.Left:
                    X -= offset;
                    break;
                case Direction.Down:
                    Y += offset;
                    break;
                case Direction.Up:
                    Y -= offset;
                    break;
                default:
                    break;
            }
        }

        // Замена точки
        public void Replace(char newCh)
        {
            Ch = newCh;
        }

        public void Replace(ConsoleColor newBgColor, ConsoleColor newFgColor)
        {
            bgColor = newBgColor;
            fgColor = newFgColor;
        }

        // Перегрузка операторов
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Ch, p1.bgColor, p1.fgColor);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Ch, p1.bgColor, p1.fgColor);
        }

        public bool IsHit(Point p)
        {
            return p.X == X && p.Y == Y;
        }

        public static Point Clone(Point p)
        {
            return new Point(p.X, p.Y, p.Ch, p.bgColor, p.fgColor);
        }

        public override string ToString()
        {
            return X + ", " + Y + ", " + Ch;
        }
    }
}
