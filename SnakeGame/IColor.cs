using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    interface IColor
    {
        void Draw(ConsoleColor bgColor, ConsoleColor fgColor);
    }
}