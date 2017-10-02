using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SnakeGame
{
    class Stats : ColorObject
    {
        // Координаты текста
        int writeX, writeY;
        // Счетчики
        internal int score;
        internal int highScore;
        internal int length;
        internal int moves;
        internal int deathCount;

        TextItem text1, text2, text3, text4, text5;

        public Stats(int writeX, int writeY, ConsoleColor bgColor, ConsoleColor fgColor) : base (bgColor, fgColor)
        {
            this.writeX = writeX;
            this.writeY = writeY;
            score = 0;
            highScore = 0;
            length = 0;
            moves = 0;
            deathCount = 0;
        }

        public void Show()
        {
            text1 = new TextItem(writeX, writeY, $"Ваш счет: {score}", bgColor, fgColor);
            text2 = new TextItem(writeX, writeY + 1, $"Ваш рекорд: {highScore}", bgColor, fgColor);
            text3 = new TextItem(writeX, writeY + 2, $"Длина змейки: {length}", bgColor, fgColor);
            text4 = new TextItem(writeX, writeY + 3, $"Шагов сделано: {moves}", bgColor, fgColor);
            text5 = new TextItem(writeX, writeY + 4, $"Смертей: {deathCount}", bgColor, fgColor);
            text1.Show();
            text2.Show();
            text3.Show();
            text4.Show();
            text5.Show();
        }

        public void SetToZero()
        {
            score = 0;
            length = 3;
            moves = 0;
        }
    }
}
