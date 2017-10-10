using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        int speed = 4;
        int length = 3;

        public static void Main()
        {
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

            Walls frame = new Walls(width, height, '*');

            // Название игры и пункты меню
            TextItem title = new TextItem(1, 1, "~~~~~~~~~~~~ЗМЕЙКА~~~~~~~~~~~~".PadBoth(31));

            // Передача пунктов в меню
            Menu menu = new Menu(new List<TextItem>
            {
                new TextItem(1, 2, "НАЧАТЬ ИГРУ".PadBoth(31)),
                new TextItem(1, 3, "НАСТРОЙКИ".PadBoth(31)),
                new TextItem(1, 4, "ВЫХОД".PadBoth(31))
            });

            // Цвета
            frame.SetColor(ConsoleColor.Black, ConsoleColor.Yellow);
            title.SetColor(ConsoleColor.DarkBlue, ConsoleColor.Green);
            menu.SetColor(ConsoleColor.DarkGreen, ConsoleColor.Green);

            // Отрисовка рамки вокруг меню
            frame.Draw();

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

            // Инициализация статистики
            Stats.Initialize(32, 10);
            Stats.Score = 0;
            Stats.Moves = 0;
            Stats.Speed = speed;
            Stats.Length = length;
            Stats.Show();

            // Отрисовка поля и стен
            Field field = new Field(columns, rows, fieldCh);
            Walls walls = new Walls(columns, rows, wallCh);

            // Отрисовка змейки
            Point tail = new Point(1, 1, 'o');
            Snake snake = new Snake(tail, Stats.Length, Direction.Right);

            // Еда
            FoodSpawner foodSpawner = new FoodSpawner(walls.Width, walls.Height, "♥♦♣♠");
            foodSpawner.Initialize();

            // Ускорение змейки в процентах
            double modifier = 5;

            // Установка цвета
            field.SetColor(ConsoleColor.DarkCyan, ConsoleColor.Cyan);
            walls.SetColor(ConsoleColor.Black, ConsoleColor.DarkGreen);
            snake.SetColor(ConsoleColor.DarkCyan, ConsoleColor.Green);
            FoodSpawner.Food.SetColor(ConsoleColor.DarkCyan, ConsoleColor.Red);

            // Отображение
            field.Draw();
            walls.Draw();
            snake.Draw();
            FoodSpawner.Food.Draw();

            // Движение змейки
            while (true)
            {
                // Выбор следующего направления
                snake.HandleKey();

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
                    FoodSpawner.Food.Draw();
                    // Увеличение очков
                    Stats.Score += 10;
                    // Параллельное увеличение рекорда
                    if (Stats.Score >= Stats.HighScore)
                        Stats.HighScore = Stats.Score;
                    // Увеличение длины
                    Stats.Length++;
                    Stats.Speed += Stats.Speed / 100 * modifier;
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
                Thread.Sleep(1000 / (int)Stats.Speed);
            }
        }

        public void Settings()
        {
            Console.Clear();

            // Всё то же, что и в главном меню
            TextItem title = new TextItem(1, 0, "~~~~~~~~~~~НАСТРОЙКИ~~~~~~~~~~~".PadBoth(31));

            Menu menu = new Menu(new List<TextItem>
            {
                new TextItem(1, 1, "СКОРОСТЬ ЗМЕЙКИ".PadBoth(31)),
                new TextItem(1, 2, "ДЛИНА ЗМЕЙКИ".PadBoth(31)),
                new TextItem(1, 3, "ВЕРНУТЬСЯ".PadBoth(31)),
            });

            title.SetColor(ConsoleColor.Black, ConsoleColor.White);
            menu.SetColor(ConsoleColor.Black, ConsoleColor.Green);

            title.Show();
            menu.Show();

            int selected = menu.SelectItem();

            switch (selected)
            {
                case 0:
                    Console.WriteLine("Текущая скорость змейки: " + this.speed);
                    Console.Write("Установите скорость змейки: ");
                    if (int.TryParse(Console.ReadLine(), out int speed))
                    {
                        this.speed = speed;
                    }
                    break;
                case 1:
                    Console.WriteLine("Текущая длина змейки: " + this.length);
                    Console.Write("Установите новую длину: ");
                    if (int.TryParse(Console.ReadLine(), out int length))
                    {
                        this.length = length;
                    }
                    break;
                case 2:
                    MenuSelect();
                    break;
            }
            Settings();
        }

        public void GameOver()
        {
            // Установка нового рекорда
            if (Stats.Score > Stats.HighScore)
                Stats.HighScore = Stats.Score;

            // Увеличение счетчика смертей
            Stats.DeathCount++;

            // Game Over текст
            List<TextItem> text = new List<TextItem>
            {
                new TextItem(10, 5, "============================".PadBoth(33)),
                new TextItem(10, 6, "И Г Р А    О К О Н Ч Е Н А".PadBoth(33)),
                new TextItem(10, 7, $"ВАШ СЧЕТ: {Stats.Score}".PadBoth(33)),
                new TextItem(10, 8, $"ВАШ РЕКОРД: {Stats.HighScore}".PadBoth(33)),
                new TextItem(10, 9, "============================".PadBoth(33))
            };

            Menu gameOver = new Menu(text);

            gameOver.SetColor(ConsoleColor.DarkRed, ConsoleColor.Yellow);

            gameOver.Show();
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