using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Snake : Figure
    {
        // Символы змейки
        char headCh;
        char tailCh;
        // Длина
        internal int length;
        // Направление
        Direction direction;

        public Snake(Point tailP, char headCh, int length, ConsoleColor bgColor, ConsoleColor fgColor, Direction direction) : base(bgColor, fgColor)
        {
            this.headCh = headCh;
            tailCh = tailP.Ch;
            pList = new List<Point>();
            this.direction = direction;

            for (int i = 0; i < length; i++)
            {
                Point p = Point.Clone(tailP);
                this.length = length;
                p.Move(i, direction);
                if (i == length - 1)
                {
                    Point head = new Point(p.X, p.Y, headCh, bgColor, fgColor);
                    pList.Add(head);
                }
                else
                {
                    pList.Add(p);
                }
            }
        }

        public void Move(char ch = ' ', ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            Point tail = pList.First();
            pList.Remove(tail);
            tail.Replace(ch);

            Point previous = pList.Last();

            Point head = GetNextPoint();
            pList.Add(head);

            // Внешние изменения
            previous.Replace(tailCh);
            previous.Draw();
            tail.Draw();
            head.Draw();
        }

        public bool Eat(Point food)
        {
            Point previous = pList.Last();

            Point head = GetNextPoint();

            if (head.IsHit(food))
            {
                previous.Replace(tailCh);
                previous.Draw();
                pList.Add(head);
                head.Draw();
                return true;
            }
            return false;
        }

        private Point GetNextPoint()
        {
            Point head = pList.Last();
            Point nextPoint = Point.Clone(head);
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        public void HandleKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (direction != Direction.Right)
                        direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (direction != Direction.Left)
                        direction = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    if (direction != Direction.Down)
                        direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (direction != Direction.Up)
                        direction = Direction.Down;
                    break;
            }
        }

        public bool IsHitTail()
        {
            Point head = pList.Last();
            for (int i = 0; i < pList.Count - 2; i++) // Проверка для всех точек кроме головы
            {
                if (head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }
    }
}
