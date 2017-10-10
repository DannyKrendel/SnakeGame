using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Figure : Colored
    {
        // Список точек фигуры
        protected List<Point> pList;

        // Рисование точек фигуры
        public void Draw()
        {
            foreach (Point p in pList)
            {
                p.SetColor(bgColor, fgColor);
                p.Draw();
            }
        }
        // Проверка столкновения фигуры с фигурой
        internal bool IsHit(Figure figure)
        {
            foreach (var p in pList)
            {
                if (figure.IsHit(p))
                    return true;
            }
            return false;
        }
        // Проверка столкновения фигуры с точкой
        internal bool IsHit(Point point)
        {
            foreach (var p in pList)
            {
                if (p.IsHit(point))
                    return true;
            }
            return false;
        }
    }
}
