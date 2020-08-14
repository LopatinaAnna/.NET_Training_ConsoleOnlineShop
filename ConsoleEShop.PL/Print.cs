using System;

namespace ConsoleEShop.PL
{
    /// <summary>
    /// Print in console
    /// </summary>
    public static class Print
    {
        /// <summary>
        /// Print header
        /// </summary>
        /// <param name="headerContent"></param>
        /// <param name="color"></param>
        public static void PrintHeader(string headerContent, ConsoleColor color)
        {
            int padding = (60 - headerContent.Length) / 2;
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + "".PadRight(60, '*'));
            Console.WriteLine("".PadRight(padding, ' ') + $"{headerContent}" + "".PadRight(padding, ' '));
            Console.WriteLine("".PadRight(60, '*') + "\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Read input
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Input string in console</returns>
        public static string ReadInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
