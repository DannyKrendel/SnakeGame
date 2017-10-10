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

        // Длина змейки
        int length;
        public int Length
        {
            get => length;
            set
            {
                if (value > 0)
                    length = value;
            }
        }

        // Направление змейки
        public Direction Direction{ get; set; }

        // Точка начала змейки (хвост), начальная длина, начальное направление
        public Snake(Point p, int length, Direction direction)
        {
            pList = new List<Point>();

            bodyCh = p.Ch;

            Length = length;

            Direction = direction;

            // Добавление точек в список
            for (int i = 0; i < Length; i++)
            {
                Point body = new Point(p);
                body.Move(i, direction);
                body.SetColor(bgColor, fgColor);
                pList.Add(body);
            }
        }

        // Движение змейки
        public void Move()
        {
            Point tail = pList.First();
            Point head = GetNextPoint();
            pList.Remove(tail);
            pList.Add(head);
            // Стирание хвоста
            tail.Undraw();
            // Рисование головы
            head.SetColor(bgColor, fgColor);
            head.Draw();
        }

        // Проверка поглощения пищи
        public bool Eat(Point food)
        {
            Point head = GetNextPoint();

            if (head.IsHit(food))
            {
                pList.Add(head);
                head.SetColor(bgColor, fgColor);
                head.Draw();
                return true;
            }
            return false;
        }

        // Следующая точка после головы в текущем направлении
        public Point GetNextPoint()
        {
            Point head = pList.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, Direction);
            return nextPoint;
        }

        // Выбор направления
        public void HandleKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (Direction != Direction.Right)
                            Direction = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (Direction != Direction.Left)
                            Direction = Direction.Right;
                        break;
                    case ConsoleKey.UpArrow:
                        if (Direction != Direction.Down)
                            Direction = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (Direction != Direction.Up)
                            Direction = Direction.Down;
                        break;
                }
            }
        }

        // Столкнулась ли змейка со стеной
        public bool IsWallHit(int width, int height)
        {
            Point head = GetNextPoint();
            return head.X == 0 || head.X == width || head.Y == 0 || head.Y == height;
        }

        // Столкнулась ли змейка с хвостом
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
