using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Menu : ColorObject
    {
        internal List<TextItem> items; // Пункты меню
        int selectedItem = 0; // Выбранный пункт

        public Menu(List<TextItem> items, ConsoleColor bgColor, ConsoleColor fgColor) : base(bgColor, fgColor)
        {
            this.items = new List<TextItem>(items);
        }

        public Menu(int x, int y, string[] str, ConsoleColor bgColor, ConsoleColor fgColor) : base(bgColor, fgColor)
        {
            Console.SetCursorPosition(x, y);
            items = new List<TextItem>();
            for (int i = 0; i < str.Length; i++)
            {
                TextItem item = new TextItem(x, y++, str[i], bgColor, fgColor);
                items.Add(item);
            }
        }

        public void Show() // Вывод меню
        {
            foreach (TextItem item in items)
            {
                item.Show();
            }
        }

        public TextItem SelectItem(ConsoleKey cKey, out int index)
        {
            items[selectedItem].ReverseColors();
            items[selectedItem].Show();

            if (cKey == ConsoleKey.DownArrow && selectedItem + 1 < items.Count)
            {
                selectedItem++;
            }
            if (cKey == ConsoleKey.UpArrow && selectedItem > 0)
            {
                selectedItem--;
            }

            items[selectedItem].ReverseColors();
            items[selectedItem].Show();

            index = selectedItem;

            return items[selectedItem];
        }
    }
}
