using ConsoleEShop.BLL;
using ConsoleEShop.DAL;
using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ConsoleEShop.Tests
{
    internal class AdminTests
    {
        private Mock<IDataContext> context;
        private GuestLogic guest;
        private AdminLogic admin;

        [SetUp]
        public void Setup()
        {
            context = new Mock<IDataContext>();

            context.Setup(x => x.UsersList).Returns(new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "user1",
                    Email = "user1@gmail.com",
                    Password = "1111",
                    ShoppingCart = new ShoppingCart(),
                    OrderList = new List<Order>()
                },
                new User()
                {
                    Id = 2,
                    Name = "user2",
                    Email = "user2@gmail.com",
                    Password = "2222",
                    ShoppingCart = new ShoppingCart(),
                    OrderList = new List<Order>()
                }
            });

            context.Setup(x => x.AdminsList).Returns(new List<Admin>()
            {
                new Admin()
                {
                    Id = 1,
                    Name = "admin",
                    Email = "admin@gmail.com",
                    Password = "0000",
                    ShoppingCart = new ShoppingCart(),
                    OrderList = new List<Order>()
                }
            });

            context.Setup(x => x.ProductsList).Returns(new List<Product>() {
                new Product()
                {
                    Category = Category.Phones,
                    Name = "Samsung Galaxy A51",
                    Price = 300,
                    Id = 1
                },
                new Product()
                {
                    Category = Category.Phones,
                    Name = "Xiaomi Redmi Note 9 Pro",
                    Price = 290,
                    Id = 2
                },
                new Product()
                {
                    Category = Category.Notebooks,
                    Name = "Lenovo IdeaPad 330",
                    Price = 350,
                    Id = 3
                }
            });

            guest = new GuestLogic(context.Object);
            admin = new AdminLogic(context.Object);
            guest.LogIn(context.Object.AdminsList[0].Email, context.Object.AdminsList[0].Password);
        }

        [Test]
        public void ViewAllUsers_ReturnCollection()
        {
            // arrange
            List<string> usersList;

            // act
            usersList = admin.ViewAllUsers();

            // assert
            Assert.IsNotEmpty(usersList);
        }

        [TestCase("NewName1", 1)]
        [TestCase("NewName2", 2)]
        public void ChangeUserName_ReturnTrue(string name, int userId)
        {
            // arrange
            string newName = name;
            int id = userId;

            // act
            bool actualResult = admin.ChangeUserName(newName, id);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase("v1", 1)]
        [TestCase("", 2)]
        public void ChangeUserName_ReturnFalse(string name, int userId)
        {
            // arrange
            string newName = name;
            int id = userId;

            // act
            bool actualResult = admin.ChangeUserName(newName, id);

            // assert
            Assert.AreEqual(false, actualResult);
        }

        [TestCase("1", "New Product 1", 100)]
        [TestCase("2", "New Product 2", 200.50)]
        public void AddNewProduct_ReturnTrue(string category, string name, decimal price)
        {
            // arrange
            string pCategory = category;
            string pName = name;
            decimal pPrice = price;

            // act
            bool actualResult = admin.AddNewProduct(pCategory, pName, pPrice);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase("-1", "New Product", 100)]
        [TestCase("1", "New Product", 0)]
        [TestCase("1", "", 100)]
        [TestCase("1", "Samsung Galaxy A51", 300)]
        public void AddNewProduct_ReturnFalse(string category, string name, decimal price)
        {
            // arrange
            string pCategory = category;
            string pName = name;
            decimal pPrice = price;

            // act
            bool actualResult = admin.AddNewProduct(pCategory, pName, pPrice);

            // assert
            Assert.AreEqual(false, actualResult);
        }

        [Test]
        public void ChangeProductName_ReturnTrue()
        {
            // arrange
            int id = 1;
            string newName = "New Name";

            // act
            bool actualResult = admin.ChangeProductName(id, newName);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase(1, "")]
        [TestCase(-1, "")]
        public void ChangeProductName_ReturnFalse(int id, string name)
        {
            // arrange
            int productId = id;
            string newName = name;

            // act
            bool actualResult = admin.ChangeProductName(productId, newName);

            // assert
            Assert.AreEqual(false, actualResult);
        }
        [Test]
        public void ChangeProductPrice_ReturnTrue()
        {
            // arrange
            int productId = 1;
            decimal newPrice = 100;

            // act
            bool actualResult = admin.ChangeProductPrice(productId, newPrice);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase(1, 0)]
        [TestCase(-1, 100)]
        public void ChangeProductPrice_ReturnFalse(int id, decimal price)
        {
            // arrange
            int productId = id;
            decimal newPrice = price;

            // act
            bool actualResult = admin.ChangeProductPrice(productId, newPrice);

            // assert
            Assert.AreEqual(false, actualResult);
        }
    }
}