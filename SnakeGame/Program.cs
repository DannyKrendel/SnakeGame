using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        // Скорость змейки
        public static double Speed = 4;

        public static void Main()
        {
            // Инициализация статистики
            Stats.Initialize(32, 11);

            // Убрать курсор
            Console.CursorVisible = false;

            // Вызов меню
            Program p = new Program();
            p.MenuSelect();
        }

        public void MenuSelect()
        {
            Console.Clear();

            // Установка размера окна меню
            int width = 33;
            int height = 6;
            Console.SetWindowSize(width + 1, height + 1);

            // Отрисовка рамки вокруг меню
            Walls frame = new Walls(width, height, '*');
            frame.Draw(ConsoleColor.Black, ConsoleColor.Yellow);

            // Название игры и пункты меню
            TextItem title = new TextItem(1, 1, "~~~~~~~~~~~~ЗМЕЙКА~~~~~~~~~~~~".PadBoth(31), ConsoleColor.DarkBlue, ConsoleColor.Green);

            List<TextItem> items = new List<TextItem>
            {
                new TextItem(1, 2, "НАЧАТЬ ИГРУ".PadBoth(31), ConsoleColor.DarkGreen, ConsoleColor.Green),
                new TextItem(1, 3, "НАСТРОЙКИ".PadBoth(31),   ConsoleColor.DarkGreen, ConsoleColor.Green),
                new TextItem(1, 4, "ВЫХОД".PadBoth(31),       ConsoleColor.DarkGreen, ConsoleColor.Green)
            };

            // Передача пунктов в функцию
            Menu menu = new Menu(items);

            // Показ названия и меню
            title.Show();
            menu.Show();

            // Индекс выбранного пункта
            int selected = menu.SelectItem();

            switch (selected)
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

            // Установка размера окна игры
            int width = 55;
            int height = 15;
            Console.SetWindowSize(width + 1, height + 1);

            // Размеры поля и стен
            int columns = 30;
            int rows = 15;

            // Отрисовка поля и стен
            Field field = new Field(columns, rows, fieldCh);
            field.Draw(ConsoleColor.DarkCyan, ConsoleColor.Cyan);
            Walls walls = new Walls(columns, rows, wallCh);
            walls.Draw(ConsoleColor.Black, ConsoleColor.DarkGreen);

            // Обнуление и отрисовка статистики
            Stats.SetToZero();
            Stats.Show();

            // Отрисовка змейки
            Point tail = new Point(1, 1, 'o');
            Snake snake = new Snake(tail, 3, Direction.Right, ConsoleColor.DarkCyan, ConsoleColor.Green);
            snake.Draw(ConsoleColor.DarkCyan, ConsoleColor.Green);

            // Передача длины змейки
            Stats.Length = Snake.Length;

            // Еда
            FoodSpawner foodSpawner = new FoodSpawner(walls.Width, walls.Height, '♦');
            foodSpawner.Initialize();
            FoodSpawner.Food.Draw(ConsoleColor.DarkCyan, ConsoleColor.Red);

            // Установка скорости по умолчанию
            double speed = Speed;

            // Ускорение в процентах
            double modifier = 5;

            // Движение змейки
            while (true)
            {
                // Выбор следующего направления
                if (Console.KeyAvailable)
                {
                    ConsoleKey direction = Console.ReadKey(true).Key;
                    snake.HandleKey(direction);
                }

                // Условия смерти змейки
                if (snake.IsWallHit(walls.Width, walls.Height) || snake.IsTailHit())
                {
                    GameOver();
                    Thread.Sleep(2000);
                    MenuSelect();
                }
                // Змейка ест
                if (snake.Eat(FoodSpawner.Food))
                {
                    do
                    {
                        foodSpawner.Initialize();
                    } while (snake.IsHit(FoodSpawner.Food));
                    FoodSpawner.Food.Draw(ConsoleColor.DarkCyan, ConsoleColor.Red);
                    // Увеличение очков
                    Stats.Score += 10;
                    // Параллельное увеличение рекорда
                    if (Stats.Score >= Stats.HighScore)
                        Stats.HighScore = Stats.Score;
                    // Увеличение длины
                    Stats.Length++;
                    Speed += Speed / 100 * modifier;
                }
                else
                {
                    // Движение змейки
                    snake.Move();
                    // Увеличение шагов
                    Stats.Moves++;
                }

                // Показ статистики
                Stats.Show();
                // Установка скорости
                Thread.Sleep(1000 / (int)speed);
            }
        }

        public void Settings()
        {
            Console.Clear();
            // Инициализация пунктов настройки
            List<TextItem> itemList = new List<TextItem>
            {
                new TextItem(1, 1, "СКОРОСТЬ ЗМЕЙКИ".PadBoth(31), ConsoleColor.Black, ConsoleColor.Green),
                new TextItem(1, 2, "ВЕРНУТЬСЯ".PadBoth(31),       ConsoleColor.Black, ConsoleColor.Green),
            };

            TextItem title = new TextItem(1, 0, "~~~~~~~~~~~НАСТРОЙКИ~~~~~~~~~~~".PadBoth(31), ConsoleColor.Black, ConsoleColor.White);

            Menu menu = new Menu(itemList);

            // Всё то же, что и в главном меню
            title.Show();
            menu.Show();

            int selected = menu.SelectItem();

            switch (selected)
            {
                case 0:
                    Console.WriteLine("Текущая скорость змейки: " + Speed);
                    Console.Write("Установите скорость змейки: ");
                    if (int.TryParse(Console.ReadLine(), out int speed))
                    {
                        Speed = speed;
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
            // Установка нового рекорда
            if (Stats.Score > Stats.HighScore)
                Stats.HighScore = Stats.Score;

            // Увеличение счетчика смертей
            Stats.DeathCount++;

            // Game Over текст
            TextItem line1 = new TextItem(10, 5, "============================".PadBoth(33),   ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line2 = new TextItem(10, 6, "И Г Р А    О К О Н Ч Е Н А".PadBoth(33),     ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line3 = new TextItem(10, 7, $"ВАШ СЧЕТ: {Stats.Score}".PadBoth(33),       ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line4 = new TextItem(10, 8, $"ВАШ РЕКОРД: {Stats.HighScore}".PadBoth(33), ConsoleColor.DarkRed, ConsoleColor.Yellow);
            TextItem line5 = new TextItem(10, 9, "============================".PadBoth(33),   ConsoleColor.DarkRed, ConsoleColor.Yellow);

            line1.Show();
            line2.Show();
            line3.Show();
            line4.Show();
            line5.Show();
        }
        // Выход из игры
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
        // Выравнивание текста по центру
        public static string PadBoth(this string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
    }
}