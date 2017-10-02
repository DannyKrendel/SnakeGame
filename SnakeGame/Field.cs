using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Field : ColorObject
    {
        // Символ поля
        char fieldCh;
        // Размер поля
        int columns;
        int rows;

        public Field(int columns, int rows, char fieldCh, ConsoleColor bgColor, ConsoleColor fgColor) : base(bgColor, fgColor)
        {
            this.fieldCh = fieldCh;
            this.columns = columns;
            this.rows = rows;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < rows; i++)
            {
                Console.BackgroundColor = bgColor;
                Console.ForegroundColor = fgColor;
                Console.WriteLine(new string(fieldCh, columns));
            }
        }

        public void GetLimits(out Point p1, out Point p2)
        {          
            p1 = new Point(0, 0, fieldCh, bgColor, fgColor);
            p2 = new Point(columns, rows, fieldCh, bgColor, fgColor);
        }
    }
}
