using ConsoleEShop.DAL;
using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using System.Collections.Generic;

namespace ConsoleEShop.BLL
{
    /// <summary>
    /// Defines user functionality
    /// </summary>
    public class UserLogic : BaseLogic
    {
        /// <summary>
        /// Defines a new instance of the class
        /// </summary>
        public UserLogic() : base()
        {
        }

        /// <summary>
        /// Defines a new instance of the class and sets context by parameter
        /// </summary>
        /// <param name="context"></param>
        public UserLogic(IDataContext context) : base(context)
        {
        }

        /// <summary>
        /// Defines unique order id
        /// </summary>
        private int OrderId => Account.OrderList.Count + 1;

        /// <summary>
        /// Returns account name
        /// </summary>
        /// <returns></returns>
        public string GetAccountName() => Account.Name;

        /// <summary>
        /// Returns account type
        /// </summary>
        /// <returns></returns>
        public string GetAccountType() => Account is User ? "USER" : "ADMIN";

        /// <summary>
        /// Sets null account when log out
        /// </summary>
        public void LogOut() => Account = null;

        /// <summary>
        /// View the list of products in shopping cart
        /// </summary>
        /// <returns>New object which is list of products</returns>
        public List<string> ViewShoppingCart()
        {
            List<string> products = new List<string>();

            if (Account.ShoppingCart is null)
                return products;

            var getProducts = Account.ShoppingCart.GetProductsInCart();

            foreach (var product in getProducts)
            {
                products.Add($"{product.Name} - price: {product.Price}$ (ID: {product.Id})");
            }

            return products;
        }

        /// <summary>
        /// Add to shopping cart
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>True if product exist in product list, false if doesn't</returns>
        public bool AddToShoppingCart(int productId)
        {
            Product product = Context.ProductsList.Find(x => x.Id == productId);

            if (product is null)
                return false;

            Account.ShoppingCart.ProductsInCart.Add(product);
            return true;
        }

        /// <summary>
        /// Remove from shopping cart
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>True if product exist in shopping cart, false if it doesn't</returns>
        public bool RemoveFromShoppingCart(int productId)
        {
            Product product = Account.ShoppingCart.GetProductsInCart().Find(x => x.Id == productId);

            if (product is null)
                return false;

            Account.ShoppingCart.ProductsInCart.Remove(product);
            return true;
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <returns>True if shopping cart was not empty, false if it was</returns>
        public bool CreateOrder()
        {
            if (Account.ShoppingCart.GetProductsInCart().Count == 0)
                return false;

            Order order = new Order()
            {
                Id = OrderId,
                Products = new List<Product>(),
                Status = OrderStatus.New
            };

            foreach (var item in Account.ShoppingCart.ProductsInCart)
            {
                order.Products.Add(item);
            }

            Account.OrderList.Add(order);

            Account.ShoppingCart.ProductsInCart.Clear();

            return true;
        }

        /// <summary>
        /// View orders
        /// </summary>
        /// <returns>New object which is list of user orders</returns>
        public List<string> ViewOrders()
        {
            List<string> orders = new List<string>();

            var getOrders = Account.OrderList;

            if (getOrders is null)
                return orders;

            foreach (var order in getOrders)
            {
                orders.Add($"Order #{order.Id}: status '{order.Status}'");
                foreach (var product in order.Products)
                {
                    orders.Add($"   {product.Name} - price: {product.Price}$ (ID: {product.Id})");
                }
                orders.Add("");
            }

            return orders;
        }

        /// <summary>
        /// Change user name
        /// </summary>
        /// <param name="name">New name</param>
        /// <returns>True if new name was correct, false if it wasn't</returns>
        public bool ChangeName(string name)
        {
            if (name.Length < 3)
                return false;

            Account.Name = name;
            return true;
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="password">New password</param>
        /// <returns>True if new password was correct, false if it wasn't</returns>
        public bool ChangePassword(string password)
        {
            if (password.Length < 3)
                return false;

            Account.Password = password;
            return true;
        }

        /// <summary>
        /// Cancel order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>
        /// True if order with <see cref="orderId"/> was find and wasn't have completed status
        /// </returns>
        public bool CancelOrder(int orderId)
        {
            Order order = Account.OrderList.Find(x => x.Id == orderId);

            if (order is null)
                return false;

            if (order.Status == OrderStatus.Completed)
                return false;

            order.Status = OrderStatus.CanceledByUser;
            return true;
        }

        /// <summary>
        /// Receiving order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>
        /// True if order with <see cref="orderId"/> was find and wasn't have completed status
        /// </returns>
        public bool ReceivingOrder(int orderId)
        {
            Order order = Account.OrderList.Find(x => x.Id == orderId);

            if (order is null)
                return false;

            if (order.Status == OrderStatus.Completed)
                return false;

            order.Status = OrderStatus.Received;
            return true;
        }
    }
}
