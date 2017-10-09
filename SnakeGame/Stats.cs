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
        public static int Moves { get; set; }
        public static int DeathCount { get; set; }

        public static void Initialize(int writeX, int writeY)
        {
            X = writeX;
            Y = writeY;

            Score = 0;
            HighScore = 0;
            Length = Snake.Length;
            Moves = 0;
            DeathCount = 0;
        }

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
            Console.WriteLine($"Пройденное расстояние: {Moves}");
            Console.CursorLeft = X;
            Console.Write($"Смертей: {DeathCount}");

            Console.ResetColor();
        }

        public static void SetToZero()
        {
            Score = 0;
            Length = 3;
            Moves = 0;
            DeathCount = 0;
        }
    }
}
