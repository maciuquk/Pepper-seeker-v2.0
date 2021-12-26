using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepper_seeker_v2._0
{
    internal class WriteText
    {
        public static void Color(string foregroundColor, string backgroundColor, string text)
        {
            ConsoleColor foreColor = ConsoleColor.White;
            ConsoleColor backColor = ConsoleColor.Black;

            switch (foregroundColor)
            {
                case "Black": foreColor = ConsoleColor.Black;
                break;
                case "Blue": foreColor = ConsoleColor.Blue;
                break;
                case "Red": foreColor = ConsoleColor.Red;
                break;
                default:
                break;
            }
            switch (backgroundColor)
            {
                case "Black": backColor = ConsoleColor.Black;
                break;
                case "Blue": backColor = ConsoleColor.Blue;
                break;
                case "Red": backColor = ConsoleColor.Red;
                break;
                default:
                break;
            }

            var beforeForegroundColor = Console.ForegroundColor;
            var beforeBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;

            Console.WriteLine(text);

            Console.ForegroundColor = backColor;
            Console.BackgroundColor = beforeBackgroundColor;


        }
    }
}
