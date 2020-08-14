using System;
using System.Net.Mail;

namespace ConsoleEShop.PL
{
    /// <summary>
    /// Parse input
    /// </summary>
    public static class ParseInput
    {
        /// <summary>
        /// Defines min value numnber os characters in input
        /// </summary>
        private const int numberOfChar = 3;

        /// <summary>
        /// Defines output string for incorrect id input
        /// </summary>
        private const string incorrectId = "Incorrect ID value!";

        /// <summary>
        /// Defines output string for incorrect name input
        /// </summary>
        private const string incorrectName = "Incorrect name value!";

        /// <summary>
        /// Defines output string for incorrect password input
        /// </summary>
        private const string incorrectPassword = "Incorrect password value!";

        /// <summary>
        /// Defines output string for incorrect price input
        /// </summary>
        private const string incorrectPrice = "Incorrect price value!";


        /// <summary>
        /// Check input name
        /// </summary>
        /// <param name="inputName"></param>
        /// <returns>True if name is  correct</returns>
        public static bool IsCorrectName(string inputName)
        {
            if (inputName.Length >= numberOfChar)
                return true;
            else
            {
                Console.WriteLine(incorrectName);
                return false;
            }
        }

        /// <summary>
        /// Check input email
        /// </summary>
        /// <param name="inputEmail"></param>
        /// <returns>True if email is correct</returns>
        public static bool IsCorrectEmail(string inputEmail)
        {
            try
            {
                _ = new MailAddress(inputEmail);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check input password
        /// </summary>
        /// <param name="inputPassword"></param>
        /// <returns>True if password is correct</returns>
        public static bool IsCorrectPassword(string inputPassword)
        {
            if (inputPassword.Length >= numberOfChar)
                return true;
            else
            {
                Console.WriteLine(incorrectPassword);
                return false;
            }
        }

        /// <summary>
        /// Check input id
        /// </summary>
        /// <param name="inputId"></param>
        /// <returns>True if id is correct</returns>
        public static int IsCorrectId(string inputId)
        {
            try
            {
                return int.Parse(inputId);
            }
            catch
            {
                Console.WriteLine(incorrectId);
                return -1;
            }
        }

        /// <summary>
        /// Check input price
        /// </summary>
        /// <param name="inputPrice"></param>
        /// <returns>True if price is correct</returns>
        public static decimal IsCorrectPrice(string inputPrice)
        {
            try
            {
                return decimal.Parse(inputPrice);
            }
            catch (Exception)
            {
                Console.WriteLine(incorrectPrice);
                return -1;
            }
        }
    }
}
