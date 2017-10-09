using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Snake : Figure
    {
        // Символ змейки
        char bodyCh;
        // Длина
        public static int Length { get; set; }
        // Направление
        Direction direction;

        ConsoleColor bgColor;
        ConsoleColor fgColor;

        public Snake(Point p, int length, Direction direction, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            pList = new List<Point>();

            bodyCh = p.Ch;

            Length = length;

            this.direction = direction;

            this.bgColor = bgColor;
            this.fgColor = fgColor;

            for (int i = 0; i < length; i++)
            {
                Point body = new Point(p);
                body.Move(i, direction);
                pList.Add(body);
            }
        }

        public void Move()
        {
            // Удалить хвост
            Point tail = pList.First();
            pList.Remove(tail);
            tail.Undraw(bgColor, fgColor);

            // Добавить голову
            Point head = GetNextPoint();
            pList.Add(head);
            head.Draw(bgColor, fgColor);
        }

        public bool Eat(Point food)
        {
            Point head = GetNextPoint();

            if (head.IsHit(food))
            {
                pList.Add(head);
                head.Draw(bgColor, fgColor);
                return true;
            }
            return false;
        }

        public Point GetNextPoint()
        {
            Point head = pList.Last();
            Point nextPoint = new Point(head);
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

        public bool IsWallHit(int width, int height)
        {
            Point head = GetNextPoint();
            return head.X == 0 || head.X == width || head.Y == 0 || head.Y == height;
        }

        public bool IsTailHit()
        {
            Point head = GetNextPoint();
            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }
    }
}
