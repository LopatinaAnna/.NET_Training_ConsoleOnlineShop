using ConsoleEShop.DAL;
using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using System;
using System.Collections.Generic;

namespace ConsoleEShop.BLL
{
    /// <summary>
    /// Defines admin functionality
    /// </summary>
    public class AdminLogic : BaseLogic
    {
        /// <summary>
        /// Defines a new instance of the class
        /// </summary>
        /// <param name="context"></param>
        public AdminLogic() : base()
        {
        }

        /// <summary>
        /// Defines a new instance of the class and sets context by parameter
        /// </summary>
        /// <param name="context"></param>
        public AdminLogic(IDataContext context) : base(context)
        {
        }

        /// <summary>
        /// Defines unique product id
        /// </summary>
        public int ProductId { get; set; } = Context.ProductsList.Count + 1;

        /// <summary>
        /// Defines  dictionary of categories
        /// </summary>
        private static readonly Dictionary<string, Enum> categories = new Dictionary<string, Enum>()
        {
            {"1", Category.Phones },
            {"2", Category.Notebooks },
            {"3", Category.SmartWatches }
        };

        /// <summary>
        /// Defines  dictionary of statuses
        /// </summary>
        private static readonly Dictionary<string, Enum> statuses = new Dictionary<string, Enum>()
        {
            {"1", OrderStatus.PaymentReceived },
            {"2", OrderStatus.Sent },
            {"3", OrderStatus.Completed },
            {"4", OrderStatus.CanceledAdministrator }
        };

        /// <summary>
        /// Change user name by admin
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <returns>True if name is correct and user exist</returns>
        public bool ChangeUserName(string name, int userId)
        {
            User user = Context.UsersList.Find(x => x.Id == userId);

            if (user is null || name.Length < 3)
                return false;

            user.Name = name;
            return true;
        }

        /// <summary>
        /// View all users
        /// </summary>
        /// <returns>New object which is list of users</returns>
        public List<string> ViewAllUsers()
        {
            List<string> users = new List<string>();

            if (Context.UsersList is null)
                return users;

            foreach (var user in Context.UsersList)
            {
                users.Add($"USER (ID: {user.Id}): {user.Name}");
                if (user.OrderList.Count == 0)
                {
                    users.Add("   -- no orders --");
                }
                else
                {
                    foreach (var order in user.OrderList)
                    {
                        users.Add($"   Order #{order.Id}: status '{order.Status}'");
                        foreach (var product in order.Products)
                        {
                            users.Add($"      {product.Name} - price: {product.Price}$ (ID: {product.Id})");
                        }
                    }
                }
                users.Add("");
            }
            return users;
        }

        /// <summary>
        /// Addd new product by admin
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns>True if parameters is correct and product with name doesn't exist</returns>
        public bool AddNewProduct(string category, string name, decimal price)
        {
            if (name.Length < 3 || price <= 0)
                return false;

            if (Context.ProductsList.Find(x => x.Name == name) != null)
                return false;

            Category productCategory = 0;
            foreach (var item in categories.Keys)
            {
                if (item.Equals(category))
                {
                    productCategory = (Category)categories[item];
                    break;
                }
            }
            if (productCategory is 0)
                return false;

            Product product = new Product() { Category = productCategory, Name = name, Price = price, Id = ProductId };
            Context.ProductsList.Add(product);
            return true;
        }

        /// <summary>
        /// Returns list of categories
        /// </summary>
        /// <returns>New object which is list of categories </returns>
        public List<string> GetCategories()
        {
            List<string> categoriesList = new List<string>();

            foreach (var item in AdminLogic.categories)
                categoriesList.Add($"[{item.Key}] {item.Value}");

            return categoriesList;
        }

        /// <summary>
        /// Change product name by admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns>True if product with id exist and name is correct</returns>
        public bool ChangeProductName(int id, string name)
        {
            Product product = Context.ProductsList.Find(x => x.Id == id);

            if (product is null || name.Length < 3)
                return false;

            product.Name = name;
            return true;
        }

        /// <summary>
        /// Change product price by admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="price"></param>
        /// <returns>True if product with id exist and price is correct</returns>
        public bool ChangeProductPrice(int id, decimal price)
        {
            if (!ProductExist(id) || price <= 0)
                return false;

            Product product = Context.ProductsList.Find(x => x.Id == id);

            product.Price = price;
            return true;
        }

        /// <summary>
        /// Returns product exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if product with id exist</returns>
        public bool ProductExist(int id)
        {
            return Context.ProductsList.Find(x => x.Id == id) != null;
        }

        /// <summary>
        /// Returns user exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if user with id exist</returns>
        public bool UserExist(int id)
        {
            return Context.UsersList.Find(x => x.Id == id) != null;
        }

        /// <summary>
        /// Returns order exist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderId"></param>
        /// <returns>True if user with userId exist and order with orderId exist</returns>
        public bool OrderExist(int userId, int orderId)
        {
            if (!UserExist(userId))
                return false;

            return Context.UsersList.Find(x => x.Id == userId).OrderList.Find(x => x.Id == orderId) != null;
        }

        /// <summary>
        /// Returns statuses
        /// </summary>
        /// <returns>New object which is list of statuses</returns>
        public List<string> GetStatuses()
        {
            List<string> statusesList = new List<string>();

            foreach (var item in statuses)
            {
                statusesList.Add($"[{item.Key}] {item.Value}");
            }

            return statusesList;
        }

        /// <summary>
        /// Change status by admin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <returns>True if status correct and such user nad order exist</returns>
        public bool ChangeStatus(int userId, int orderId, string status)
        {
            if (!OrderExist(userId, orderId))
                return false;

            OrderStatus orderStatus = 0;
            foreach (var item in statuses.Keys)
            {
                if (item.Equals(status))
                {
                    orderStatus = (OrderStatus)statuses[item];
                    break;
                }
            }
            if (orderStatus is 0)
                return false;

            Context.UsersList.Find(x => x.Id == userId).OrderList.Find(x => x.Id == orderId).Status = orderStatus;

            return true;
        }
    }
}
