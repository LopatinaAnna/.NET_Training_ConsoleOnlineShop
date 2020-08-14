using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using System.Collections.Generic;

namespace ConsoleEShop.DAL
{
    /// <summary>
    /// Defines data context
    /// </summary>
    public class DataContext : IDataContext
    {
        /// <summary>
        /// List of products
        /// </summary>
        public List<Product> ProductsList { get; set; }

        /// <summary>
        /// List of users
        /// </summary>
        public List<User> UsersList { get; set; }

        /// <summary>
        /// List of admins
        /// </summary>
        public List<Admin> AdminsList { get; set; }

        /// <summary>
        /// List of orders
        /// </summary>
        public List<Order> OrdersList { get; set; }


        /// <summary>
        /// Defines id for user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Defines id for product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Defines id for admin
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// Defines id for order
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Defines a new instance of the class and initializes lists
        /// </summary>
        public DataContext()
        {
            ProductsList = new List<Product>
            {
                new Product() { Category = Category.Phones, Name = "Samsung Galaxy A51", Price = 300, Id = ++ProductId },
                new Product() { Category = Category.Phones, Name = "Xiaomi Redmi Note 9 Pro", Price = 290, Id = ++ProductId },
                new Product() { Category = Category.Notebooks, Name = "Lenovo IdeaPad 330", Price = 350, Id = ++ProductId },
                new Product() { Category = Category.Notebooks, Name = "Apple A1466 MacBook Air", Price = 1100, Id = ++ProductId },
                new Product() { Category = Category.SmartWatches, Name = "Samsung Galaxy Watch", Price = 280, Id = ++ProductId }
            };

            OrdersList = new List<Order>
            {
                new Order()
                {
                    Id = ++OrderId,
                    Status = OrderStatus.New,
                    Products = new List<Product>(){ ProductsList[0], ProductsList[1] }
                },
                new Order
                {
                    Id = ++OrderId,
                    Status = OrderStatus.New,
                    Products = new List<Product>(){ ProductsList[2] }
                },
            };

            UsersList = new List<User>
            {
                new User()
                {
                    Id = ++UserId,
                    Name = "user1",
                    Email = "user1@gmail.com",
                    Password = "1111",
                    ShoppingCart = new ShoppingCart(){ ProductsInCart = new List<Product>(){ProductsList[1]}},
                    OrderList = new List<Order>(){ OrdersList[0], OrdersList[1] }
                },
                new User()
                {
                    Id = ++UserId,
                    Name = "user2",
                    Email = "user2@gmail.com",
                    Password = "2222",
                    ShoppingCart = new ShoppingCart(),
                    OrderList = new List<Order>()
                }
            };

            AdminsList = new List<Admin>
            {
                new Admin()
                {
                    Id = ++AdminId,
                    Name = "admin",
                    Email = "admin@gmail.com",
                    Password = "0000",
                    ShoppingCart = new ShoppingCart(),
                    OrderList = new List<Order>()
                }
            };
        }
    }
}
