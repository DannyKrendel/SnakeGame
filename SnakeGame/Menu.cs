using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Menu
    {
        // Список пунктов меню
        internal List<TextItem> items;

        // Выбранный пункт
        int selectedItem = 0;

        public int SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value >= 0 && value < items.Count)
                {
                    selectedItem = value;
                }
            }
        }

        public Menu(List<TextItem> items)
        {
            this.items = new List<TextItem>(items);
        }

        public void Show() // Вывод меню
        {
            foreach (TextItem item in items)
            {
                item.Show();
            }
        }

        public int SelectItem()
        {
            items[SelectedItem].ReverseColors();
            items[SelectedItem].Show();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    items[SelectedItem].ReverseColors();
                    items[SelectedItem].Show();
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.DownArrow:
                            SelectedItem++;
                            break;
                        case ConsoleKey.UpArrow:
                            SelectedItem--;
                            break;
                        case ConsoleKey.Enter:
                            items[SelectedItem].Flickering(6, 5);
                            return SelectedItem;
                    }
                    items[SelectedItem].ReverseColors();
                    items[SelectedItem].Show();
                }
            }
        }
    }
}
