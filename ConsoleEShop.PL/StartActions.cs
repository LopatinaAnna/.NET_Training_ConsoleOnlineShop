using ConsoleEShop.BLL;
using System;

namespace ConsoleEShop.PL
{
    /// <summary>
    /// Start actions 
    /// </summary>
    public static class StartActions
    {
        /// <summary>
        /// Guest logic
        /// </summary>
        public static GuestLogic GuestLogic { get; set; } = new GuestLogic();

        /// <summary>
        /// User logic
        /// </summary>
        public static UserLogic UserLogic { get; set; } = new UserLogic();

        /// <summary>
        /// Admin logic
        /// </summary>
        public static AdminLogic AdminLogic { get; set; } = new AdminLogic();


        /// <summary>
        /// Choose menu 'Guest'
        /// </summary>
        public static void ChooseGuest()
        {
            Console.Clear();

            UserActions.GuestMode();

            while (true)
            {
                Print.PrintHeader("GUEST", ConsoleColor.Green);
                UserActions.GetActions();

                string input = Console.ReadLine();
                Console.Clear();
                Print.PrintHeader("GUEST", ConsoleColor.Green);

                if (input.ToLower() == "q")
                {
                    break;
                }

                UserActions.ChooseAction(input);
            }
        }

        /// <summary>
        /// Choose menu 'LogIn'
        /// </summary>
        public static void ChooseLogIn()
        {
            string name, password;
            GuestLogic = new GuestLogic();

            while (true)
            {
                Console.Clear();
                Print.PrintHeader("LOG IN", ConsoleColor.DarkYellow);

                name = Print.ReadInput("\nEnter user email: ");
                password = Print.ReadInput("\nEnter password: ");

                if (!GuestLogic.LogIn(name, password))
                {
                    Console.WriteLine("\nThere is no such user, check name or password!");
                    string input = Print.ReadInput("\n[1] Try again\nor enter any other key to return");
                    if (input != "1") return;
                }
                else break;
            }

            Console.Clear();
            if (UserLogic.GetAccountType().Equals("USER"))
                UserActions.UserMode();
            else
                UserActions.AdminMode();

            while (true)
            {
                Print.PrintHeader($"{UserLogic.GetAccountType()}: {UserLogic.GetAccountName()}", ConsoleColor.Green);

                UserActions.GetActions();

                string input = Console.ReadLine();
                Console.Clear();
                Print.PrintHeader($"{UserLogic.GetAccountType()}: {UserLogic.GetAccountName()}", ConsoleColor.Green);
                if (input.ToLower() == "q")
                {
                    UserLogic.LogOut();
                    break;
                }

                UserActions.ChooseAction(input);
            }
        }

        /// <summary>
        /// Choose menu 'Register'
        /// </summary>
        public static void Register()
        {
            GuestLogic = new GuestLogic();
            string name, email, password, input;

            while (true)
            {
                Console.Clear();
                Print.PrintHeader("REGISTER", ConsoleColor.DarkYellow);

                name = Print.ReadInput("\nEnter your name (at least 3 characters): ");
                if (!ParseInput.IsCorrectName(name))
                {
                    Console.WriteLine("\nIncorrect name!");
                    input = Print.ReadInput("\n[1] Try again\nor enter any other key to return");
                    if (input != "1") return;
                    else continue;
                }

                email = Print.ReadInput("\nEnter user email (*****@***): ");
                if (!ParseInput.IsCorrectEmail(email))
                {
                    Console.WriteLine("\nIncorrect email!");
                    input = Print.ReadInput("\n[1] Try again\nor enter any other key to return");
                    if (input != "1") return;
                    else continue;
                }

                password = Print.ReadInput("\nEnter password (at least 3 characters): ");
                if (!ParseInput.IsCorrectPassword(password))
                {
                    Console.WriteLine("\nIncorrect password!");
                    input = Print.ReadInput("\n[1] Try again\nor enter any other key to return");
                    if (input != "1") return;
                    else continue;
                }
                if (!GuestLogic.Register(name, email, password))
                {
                    Console.WriteLine("\nUser with this email already exists!");
                    input = Print.ReadInput("\n[1] Try again\nor enter any other key to return");
                    if (input != "1") return;
                }
                else break;
            }

            GuestLogic.LogIn(email, password);

            Console.Clear();
            if (UserLogic.GetAccountType().Equals("USER"))
                UserActions.UserMode();
            else
                UserActions.AdminMode();

            while (true)
            {
                Print.PrintHeader($"{UserLogic.GetAccountType()}: {UserLogic.GetAccountName()}", ConsoleColor.Green);

                UserActions.GetActions();

                input = Console.ReadLine();
                Console.Clear();
                Print.PrintHeader($"{UserLogic.GetAccountType()}: {UserLogic.GetAccountName()}", ConsoleColor.Green);
                if (input.ToLower() == "q")
                {
                    UserLogic.LogOut();
                    break;
                }

                UserActions.ChooseAction(input);
            }
        }

        /// <summary>
        /// Choose menu 'Exit'
        /// </summary>
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
