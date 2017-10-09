using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class TextItem
    {
        int x;
        int y;
        string str;
        ConsoleColor bgColor;
        ConsoleColor fgColor;

        public TextItem(int x, int y, string str, ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            this.x = x;
            this.y = y;
            this.str = str;
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }

        public void Show() // Показ пункта меню
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.Write(str + "\r");
            Console.ResetColor();
        }

        public void Flickering(int n, int speed) // Мигание пункта меню
        {
            for (int i = 0; i < n; i++)
            {
                Show();
                ReverseColors();

                Thread.Sleep(1000 / speed);
            }
            Console.Clear();
        }

        public void ReverseColors()
        {
            ConsoleColor temp = bgColor;
            bgColor = fgColor;
            fgColor = temp;
        }
    }
}
