using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Field : Colored
    {
        // Символ поля
        char fieldCh;
        // Размер поля
        int columns;
        int rows;

        public Field(int columns, int rows, char fieldCh)
        {
            this.fieldCh = fieldCh;
            this.columns = columns;
            this.rows = rows;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;

            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine(new string(fieldCh, columns));
            }

            Console.ResetColor();
        }
    }
}
