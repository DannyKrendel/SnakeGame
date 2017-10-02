using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        // Статистика
        Stats stats = new Stats(32, 11, ConsoleColor.Black, ConsoleColor.White); // Статистика
        public int speed = 200; // Скорость змейки

        public static void Main()
        {
            // Убрать курсор
            Console.CursorVisible = false;

            Program p = new Program();
            p.MenuSelect();
        }

        public void MenuSelect()
        {
            Console.Clear();
            // Установка размера окна
            int width = 33;
            int height = 6;
            Console.SetWindowSize(width + 1, height + 1);

            // Отрисовка рамки
            Walls frame = new Walls(width, height, '*', ConsoleColor.Black, ConsoleColor.Yellow);
            frame.Draw();
            // Пункты меню
            TextItem title = new TextItem(1, 1, "~~~~~~~~~~~~ЗМЕЙКА~~~~~~~~~~~~".PadBoth(31), ConsoleColor.DarkBlue, ConsoleColor.Green);
            string[] items = { "НАЧАТЬ ИГРУ".PadBoth(31), "НАСТРОЙКИ".PadBoth(31), "ВЫХОД".PadBoth(31) };
            // Передача пунктов и их цвета
            Menu menu = new Menu(1, 2, items, ConsoleColor.DarkCyan, ConsoleColor.Green);
            // Показ названия и меню
            title.Show();
            menu.Show();
            // Выделить цветом и показать первый пункт
            menu.items[0].ReverseColors();
            menu.items[0].Show();

            ConsoleKey ck;
            TextItem selected;

            int idx; // Индекс выбранного пункта

            do
            {
                ck = Console.ReadKey(true).Key;
                selected = menu.SelectItem(ck, out idx);
            } while (ck != ConsoleKey.Enter);
            selected.Flickering(6, 80);

            switch (idx)
            {
                case 0:
                    Game();
                    break;
                case 1:
                    Settings();
                    break;
                case 2:
                    Exit();
                    break;
            }
        }

        public void Game()
        {
            // Символы поля и стен
            char fieldCh = ' ';
            char wallCh = '#';

            // Установка размера окна
            int width = 55;
            int height = 15;
            Console.SetWindowSize(width + 1, height + 1);

            // Размеры поля и стен
            int columns = 30;
            int rows = 15;

            // Отрисовка поля и стен
            Field field = new Field(columns, rows, fieldCh, ConsoleColor.DarkCyan, ConsoleColor.Cyan);
            field.Draw();
            Walls walls = new Walls(columns, rows, wallCh, ConsoleColor.Black, ConsoleColor.DarkGreen);
            walls.Draw();

            // Обнуление и отрисовка статистики
            stats.SetToZero();
            stats.Show();

            // Отрисовка змейки
            Point tail = new Point(1, 1, 'o', ConsoleColor.DarkCyan, ConsoleColor.Green);
            char headCh = 'O';
            Snake snake = new Snake(tail, headCh, 3, ConsoleColor.DarkCyan, ConsoleColor.Green, Direction.Right);
            snake.Draw();

            // Передача длины змейки
            stats.length = snake.length;

            // Еда
            field.GetLimits(out Point p1, out Point p2);
            FoodSpawner foodSpawner = new FoodSpawner(p1, p2, '♦', ConsoleColor.DarkCyan, ConsoleColor.Red);
            Point food = foodSpawner.Spawn();
            food.Draw();

            // Движение змейки
            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    GameOver();
                    Thread.Sleep(2000);
                    MenuSelect();
                }
                if(snake.Eat(food))
                {
                    do
                    {
                        food = foodSpawner.Spawn();
                    } while (snake.IsHit(food));
                    food.Draw();
                    // Увеличение очков
                    stats.score += 10;
                    if (stats.score >= stats.highScore)
                        stats.highScore = stats.score;
                    // Увеличение длины
                    stats.length++;
                }
                else
                {
                    snake.Move(fieldCh, ConsoleColor.DarkCyan, ConsoleColor.Cyan);
                    // Увеличение шагов
                    stats.moves++;
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKey direction = Console.ReadKey(true).Key;
                    snake.HandleKey(direction);
                }

                // Показ статистики
                stats.Show();

                Thread.Sleep(speed);
            }
        }

        public void Settings()
        {
            Console.Clear();
            List<TextItem> itemList;
            TextItem item1 = new TextItem(1, 1, "СКОРОСТЬ ЗМЕЙКИ".PadBoth(31), ConsoleColor.Black, ConsoleColor.Green);
            TextItem item2 = new TextItem(1, 2, "ВЕРНУТЬСЯ".PadBoth(31), ConsoleColor.Black, ConsoleColor.Green);

            itemList = new List<TextItem> { item1, item2 };

            Menu menu = new Menu(itemList, ConsoleColor.Black, ConsoleColor.Green);

            menu.Show();

            menu.items[0].ReverseColors();
            menu.items[0].Show();

            ConsoleKey ck;
            TextItem selected;

            int idx; // Индекс выбранного пункта

            do
            {
                ck = Console.ReadKey(true).Key;
                selected = menu.SelectItem(ck, out idx);
            } while (ck != ConsoleKey.Enter);
            selected.Flickering(6, 80);

            switch (idx)
            {
                case 0:
                    Console.Write("Установите скорость змейки: ");
                    if (int.TryParse(Console.ReadLine(), out int speed))
                    {
                        this.speed = speed;
                    }
                    Settings();
                    break;
                case 1:
                    MenuSelect();
                    break;
            }
        }

        public void GameOver()
        {
            if (stats.score > stats.highScore)
                stats.highScore = stats.score;

            stats.deathCount++;

            TextItem line1 = new TextItem(10, 5, "============================".PadBoth(33), ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line2 = new TextItem(10, 6, "И Г Р А    О К О Н Ч Е Н А".PadBoth(33), ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line3 = new TextItem(10, 7, $"ВАШ СЧЕТ: {stats.score}".PadBoth(33), ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line4 = new TextItem(10, 8, $"ВАШ РЕКОРД: {stats.highScore}".PadBoth(33), ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line5 = new TextItem(10, 9, "============================".PadBoth(33), ConsoleColor.DarkRed, ConsoleColor.Yellow);

            line1.Show();
            line2.Show();
            line3.Show();
            line4.Show();
            line5.Show();
        }

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}

namespace System
{
    public static class StringExtensions
    {
        public static string PadBoth(this string str, int length) // Выравнивание текста
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
    }
}