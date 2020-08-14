using System;
using System.Collections.Generic;

namespace ConsoleEShop.PL
{
    /// <summary>
    /// Defines start menu
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Output start actions to the console and get input
        /// </summary>
        public static void ShowActions()
        {
            while (true)
            {
                Print.PrintHeader("WELCOME TO ONLINE SHOP!", ConsoleColor.Green);
                Print.PrintHeader("Select login type", ConsoleColor.Yellow);
                GetActions();
                ChooseStartUpAction(Console.ReadLine());
                Console.Clear();
            }
        }

        /// <summary>
        /// Dictionary with start actions
        /// </summary>
        private static readonly Dictionary<string, Action> chooseStartAction = new Dictionary<string, Action> {
                { "[1] Guest", StartActions.ChooseGuest },
                { "[2] Log In", StartActions.ChooseLogIn },
                { "[3] Register", StartActions.Register },
                { "[4] Exit", StartActions.Exit }
            };

        /// <summary>
        /// Output actions in console
        /// </summary>
        public static void GetActions()
        {
            foreach (var item in chooseStartAction.Keys)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Match input key with start actions
        /// </summary>
        /// <param name="key"></param>
        public static void ChooseStartUpAction(string key)
        {
            foreach (var item in chooseStartAction)
            {
                if (item.Key.Contains($"[{key}]"))
                {
                    chooseStartAction[item.Key]();
                    break;
                }
            }
        }
    }
}
