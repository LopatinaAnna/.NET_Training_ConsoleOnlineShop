using System;
using System.Collections.Generic;

namespace ConsoleEShop.PL
{
    /// <summary>
    /// User actions
    /// </summary>
    public static class UserActions
    {
        /// <summary>
        /// Dictionary with user actions
        /// </summary>
        public static Dictionary<string, Action> UsersActions { get; set; } = new Dictionary<string, Action>();

        #region Basic functionality

        /// <summary>
        /// Choose menu "List of all products"
        /// </summary>
        public static void GetListOfProducts()
        {
            Print.PrintHeader("List of all products", ConsoleColor.Yellow);

            var productsList = StartActions.GuestLogic.GetListOfProducts();

            if (productsList.Count == 0)
            {
                Console.WriteLine("List of products is empty.");
            }
            else
            {
                foreach (var item in productsList)
                {
                    Console.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// Choose menu "Search product by name"
        /// </summary>
        public static void SearchProductbyName()
        {
            Print.PrintHeader("Search product by name", ConsoleColor.Yellow);

            string name = Print.ReadInput("Enter name of product you want to find:");

            var foundProducts = StartActions.GuestLogic.SearchProductbyName(name);

            if (foundProducts.Count == 0)
                Console.WriteLine("\nNo matches found.");
            else
            {
                Console.WriteLine("\nFound such products as:");
                foreach (var product in foundProducts)
                {
                    Console.WriteLine(product);
                }
            }
        }

        #endregion Basic functionality

        #region User functionality

        /// <summary>
        /// Choose menu "Shopping cart"
        /// </summary>
        public static void ViewShoppingCart()
        {
            Print.PrintHeader("Shopping cart", ConsoleColor.DarkYellow);

            var cart = StartActions.UserLogic.ViewShoppingCart();

            if (cart.Count == 0)
            {
                Console.WriteLine("Shopping cart is empty!");
            }
            else
            {
                foreach (var item in cart)
                {
                    Console.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// Choose menu "Add to shopping cart"
        /// </summary>
        public static void AddToShoppingCart()
        {
            string inputProductId;
            int productId;

            Print.PrintHeader("Add to shopping cart", ConsoleColor.DarkYellow);

            inputProductId = Print.ReadInput("\nEnter ID of the product you want to add to the cart: ");
            productId = ParseInput.IsCorrectId(inputProductId);

            if (productId == -1)
                return;

            if (!StartActions.UserLogic.AddToShoppingCart(productId))
                Console.WriteLine("\nSuch product doesn't exist!");
            else
            {
                Console.WriteLine($"\nProduct with ID:{productId} added!");
            }
        }

        /// <summary>
        /// Choose menu "Remove from shopping cart"
        /// </summary>
        public static void RemoveFromShoppingCart()
        {
            string inputProductId;
            int productId;

            Print.PrintHeader("Remove from shopping cart", ConsoleColor.DarkYellow);

            inputProductId = Print.ReadInput("\nEnter ID of the product you want to remove from the cart: ");
            productId = ParseInput.IsCorrectId(inputProductId);

            if (productId == -1)
                return;

            if (!StartActions.UserLogic.RemoveFromShoppingCart(productId))
                Console.WriteLine("\nThis product is not in the cart!");
            else
            {
                Console.WriteLine($"\nProduct with ID:{productId} removed!");
            }
        }

        /// <summary>
        /// Choose menu "Create order"
        /// </summary>
        public static void CreateOrder()
        {
            ViewShoppingCart();
            Print.PrintHeader("Create order", ConsoleColor.DarkYellow);
            if (StartActions.UserLogic.ViewShoppingCart().Count == 0)
            {
                Console.WriteLine("Add products to shopping cart!");
                return;
            }
            Console.WriteLine("\nThe order will be contain all products from cart.");
            string input = Print.ReadInput("\nConfirm the order? \n[1] Yes \nor enter any other key to return: ");
            if (input == "1")
            {
                StartActions.UserLogic.CreateOrder();
                Console.Write("\nOrder created!");
            }
            else Console.Write("\nOrder not created!");
        }

        /// <summary>
        /// Choose menu "View orders"
        /// </summary>
        public static void ViewOrders()
        {
            Print.PrintHeader("View orders", ConsoleColor.DarkYellow);

            var orders = StartActions.UserLogic.ViewOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("Orders history is empty!");
            }
            else
            {
                foreach (var order in orders)
                {
                    Console.WriteLine(order);
                }
            }
        }

        /// <summary>
        /// Choose menu "Cancel Order"
        /// </summary>
        public static void CancelOrder()
        {
            string inputOrderId;
            int orderId;

            Print.PrintHeader("Cancel Order", ConsoleColor.DarkYellow);

            inputOrderId = Print.ReadInput("Enter order ID:");

            orderId = ParseInput.IsCorrectId(inputOrderId);

            if (orderId == -1)
                return;

            if (!StartActions.UserLogic.CancelOrder(orderId))
                Console.WriteLine("\nSuch order doesn't exist!");
            else
                Console.WriteLine("\nOrder canceled!");
        }

        /// <summary>
        /// Choose menu "Receiving Order"
        /// </summary>
        public static void ReceivingOrder()
        {
            string inputOrderId;
            int orderId;

            Print.PrintHeader("Receiving Order", ConsoleColor.DarkYellow);

            inputOrderId = Print.ReadInput("Enter order ID:");

            orderId = ParseInput.IsCorrectId(inputOrderId);

            if (orderId == -1)
                return;

            if (!StartActions.UserLogic.ReceivingOrder(orderId))
                Console.WriteLine("\nSuch order doesn't exist!");
            else
                Console.WriteLine("\nOrder received!");
        }

        /// <summary>
        /// Choose menu ("Change username"
        /// </summary>
        public static void ChangeName()
        {
            string inputName;

            Print.PrintHeader("Change username", ConsoleColor.DarkYellow);
            inputName = Print.ReadInput("Enter new username (at least 3 characters):");

            if (!ParseInput.IsCorrectName(inputName))
                return;

            if (!StartActions.UserLogic.ChangeName(inputName))
                Console.WriteLine("Username hasn't changed!");
            else
                Console.WriteLine("Username has changed!");
        }

        /// <summary>
        /// Choose menu "Change password"
        /// </summary>
        public static void ChangePassword()
        {
            string inputPassword;

            Print.PrintHeader("Change password", ConsoleColor.DarkYellow);
            inputPassword = Print.ReadInput("Enter new password (at least 3 characters):");

            if (!ParseInput.IsCorrectPassword(inputPassword))
                return;

            if (!StartActions.UserLogic.ChangePassword(inputPassword))
                Console.WriteLine("Password  hasn't changed!");
            else
                Console.WriteLine("Password  has changed!");
        }

        #endregion User functionality

        #region Admin functionality

        /// <summary>
        /// Choose menu "Change user data"
        /// </summary>
        public static void ChangeUserDataByAdmin()
        {
            string inputName, inputUserId;
            int userId;

            Print.PrintHeader("Change user data", ConsoleColor.DarkYellow);
            inputUserId = Print.ReadInput("Enter user ID:");

            userId = ParseInput.IsCorrectId(inputUserId);
            if (userId == -1)
                return;

            inputName = Print.ReadInput("Enter new user name (at least 3 characters):");
            if (!ParseInput.IsCorrectName(inputName))
                return;

            if (!StartActions.AdminLogic.ChangeUserName(inputName, userId))
                Console.WriteLine("Username hasn't changed!");
            else
                Console.WriteLine("Username has changed!");
        }

        /// <summary>
        /// Choose menu "Change product data"
        /// </summary>
        public static void ChangeProductDataByAdmin()
        {
            string inputName, inputPrice, inputProductId;
            int productId;
            decimal price;

            Print.PrintHeader("Change product data", ConsoleColor.DarkYellow);

            inputProductId = Print.ReadInput("Enter product ID:");
            productId = ParseInput.IsCorrectId(inputProductId);
            if (productId == -1)
                return;

            if (!StartActions.AdminLogic.ProductExist(productId))
            {
                Console.WriteLine("Such product doesn't exist!");
                return;
            }

            string input = Print.ReadInput("Enter what you need to change\n[1] Product name\n[2] Product price");
            if (input == "1")
            {
                inputName = Print.ReadInput("Enter new name:");
                if (!ParseInput.IsCorrectName(inputName))
                    return;

                if (StartActions.AdminLogic.ChangeProductName(productId, inputName))
                    Console.WriteLine("Name changed!");
                else
                    Console.WriteLine("Incorrect input!");
            }
            else if (input == "2")
            {
                inputPrice = Print.ReadInput("Enter new price:");
                price = ParseInput.IsCorrectPrice(inputPrice);

                if (price == -1)
                    return;

                if (StartActions.AdminLogic.ChangeProductPrice(productId, price))
                    Console.WriteLine("Price changed!");
                else
                    Console.WriteLine("Incorrect input!");
            }
            else Console.WriteLine("Incorrect input!");
        }

        /// <summary>
        /// Choose menu "Add New Product"
        /// </summary>
        public static void AddNewProduct()
        {
            string inputName, inputPrice;
            decimal price;

            Print.PrintHeader("Add New Product", ConsoleColor.DarkYellow);

            Console.WriteLine("Choose product category: ");

            foreach (var item in StartActions.AdminLogic.GetCategories())
            {
                Console.WriteLine(item);
            }

            string inputCategory = Print.ReadInput("\nEnter category number: ");
            inputName = Print.ReadInput("Enter name (at least 3 characters):");
            if (!ParseInput.IsCorrectName(inputName))
                return;

            inputPrice = Print.ReadInput("Enter price:");
            price = ParseInput.IsCorrectPrice(inputPrice);
            if (price == -1)
                return;

            if (!StartActions.AdminLogic.AddNewProduct(inputCategory, inputName, price))
                Console.WriteLine("Incorrect input data\nProduct hasn't added!");
            else
                Console.WriteLine("Product has added!");
        }

        /// <summary>
        /// Choose menu "View All Users"
        /// </summary>
        public static void ViewAllUsers()
        {
            Print.PrintHeader("View All Users", ConsoleColor.DarkYellow);

            foreach (var item in StartActions.AdminLogic.ViewAllUsers())
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Choose menu "Change order status"
        /// </summary>
        public static void ChangeStatus()
        {
            string inputUserId, inputOrderId;
            int userId, orderId;

            Print.PrintHeader("Change order status", ConsoleColor.DarkYellow);

            inputUserId = Print.ReadInput("\nEnter user ID: ");
            userId = ParseInput.IsCorrectId(inputUserId);
            if (userId == -1)
                return;

            inputOrderId = Print.ReadInput("\nEnter order ID: ");
            orderId = ParseInput.IsCorrectId(inputOrderId);
            if (orderId == -1)
                return;

            if (!StartActions.AdminLogic.UserExist(userId))
            {
                Console.WriteLine("Such user doesn't exist!");
                return;
            }

            if (!StartActions.AdminLogic.OrderExist(userId, orderId))
            {
                Console.WriteLine("Such order doesn't exist!");
                return;
            }

            Console.WriteLine("Choose status: ");

            foreach (var item in StartActions.AdminLogic.GetStatuses())
            {
                Console.WriteLine(item);
            }

            string inputStatus = Print.ReadInput("\nEnter status number: ");

            if (!StartActions.AdminLogic.ChangeStatus(userId, orderId, inputStatus))
                Console.WriteLine("Incorrect input status!");
            else
                Console.WriteLine("Status changed!");
        }

        #endregion Admin functionality


        /// <summary>
        /// Output user actions in console
        /// </summary>
        public static void GetActions()
        {
            foreach (var item in UsersActions.Keys)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("[Q] Return to main menu");
        }

        /// <summary>
        /// Match input key with user actions
        /// </summary>
        /// <param name="input"></param>
        public static void ChooseAction(string input)
        {
            foreach (var item in UsersActions)
            {
                if (item.Key.Contains($"[{input}]"))
                {
                    UsersActions[item.Key]();
                    break;
                }
            }
        }

        /// <summary>
        /// Number of menu item
        /// </summary>
        private static int i;

        /// <summary>
        /// Output menu items when user is 'Guest'
        /// </summary>
        public static void GuestMode()
        {
            i = 0;
            UsersActions.Clear();
            UsersActions.Add($"[{++i}] View the list of products", GetListOfProducts);
            UsersActions.Add($"[{++i}] Search the product by name", SearchProductbyName);
        }

        /// <summary>
        /// Output menu items when user is 'User'
        /// </summary>
        public static void UserMode()
        {
            GuestMode();
            UsersActions.Add($"[{++i}] Create an order", CreateOrder);
            UsersActions.Add($"[{++i}] Cancel an order", CancelOrder);
            UsersActions.Add($"[{++i}] Check receiving order", ReceivingOrder);
            UsersActions.Add($"[{++i}] View orders", ViewOrders);
            UsersActions.Add($"[{++i}] View shopping cart", ViewShoppingCart);
            UsersActions.Add($"[{++i}] Add to shopping cart", AddToShoppingCart);
            UsersActions.Add($"[{++i}] Remove from shopping cart", RemoveFromShoppingCart);
            UsersActions.Add($"[{++i}] Change username", ChangeName);
            UsersActions.Add($"[{++i}] Change password", ChangePassword);
        }

        /// <summary>
        /// Output menu items when user is 'Admin'
        /// </summary>
        public static void AdminMode()
        {
            UserMode();
            UsersActions.Add($"\nSHOP MANAGEMENT\n", default);
            UsersActions.Add($"[{++i}] View all users", ViewAllUsers);
            UsersActions.Add($"[{++i}] Change user data", ChangeUserDataByAdmin);
            UsersActions.Add($"[{++i}] Add new product", AddNewProduct);
            UsersActions.Add($"[{++i}] Change product data", ChangeProductDataByAdmin);
            UsersActions.Add($"[{++i}] Change user order status", ChangeStatus);
        }
    }
}