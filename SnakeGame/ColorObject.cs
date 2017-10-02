using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    class ColorObject
    {
        protected ConsoleColor bgColor;
        protected ConsoleColor fgColor;

        public ColorObject(ConsoleColor bgColor, ConsoleColor fgColor)
        {
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }
    }
}
