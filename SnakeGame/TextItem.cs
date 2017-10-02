using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class TextItem : ColorObject
    {
        int x;
        int y;
        string str;

        public TextItem(int x, int y, string str, ConsoleColor bgColor, ConsoleColor fgColor) : base(bgColor, fgColor)
        {
            this.x = x;
            this.y = y;
            this.str = str;
        }

        public void Show() // Показ пункта меню
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.SetCursorPosition(x, y);
            Console.Write(str + "\r");
            Console.ResetColor();
        }

        public void Flickering(int n, int speed) // Мигание пункта меню
        {
            for (int i = 0; i < n * 2; i++)
            {
                if (i % 2 == 0)
                {
                    Show();
                    ReverseColors();
                }
                else
                {
                    Show();
                }
                Thread.Sleep(speed);
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
