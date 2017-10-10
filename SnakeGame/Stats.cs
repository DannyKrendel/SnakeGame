using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    static class Stats
    {
        // Координаты текста
        public static int X { get; set; }
        public static int Y { get; set; }
        // Счетчики
        public static int Score { get; set; }
        public static int HighScore { get; set; }
        public static int Length { get; set; }
        public static double Speed { get; set; }
        public static int Moves { get; set; }
        public static int DeathCount { get; set; }

        public static void Initialize(int writeX, int writeY)
        {
            X = writeX;
            Y = writeY;

            Score = 0;
            Moves = 0;
        }

        // Вывод статистики
        public static void Show(ConsoleColor bgColor = ConsoleColor.Black, ConsoleColor fgColor = ConsoleColor.White)
        {
            Console.SetCursorPosition(X, Y);

            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;

            Console.WriteLine($"Ваш счет: {Score}");
            Console.CursorLeft = X;
            Console.WriteLine($"Ваш рекорд: {HighScore}");
            Console.CursorLeft = X;
            Console.WriteLine($"Длина змейки: {Length}");
            Console.CursorLeft = X;
            Console.WriteLine($"Скорость змейки: {Speed.ToString("#.##")}");
            Console.CursorLeft = X;
            Console.WriteLine($"Клеток пройдено: {Moves}");
            Console.CursorLeft = X;
            Console.Write($"Смертей: {DeathCount}");

            Console.ResetColor();
        }
    }
}
