using ConsoleEShop.BLL;
using ConsoleEShop.DAL;
using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ConsoleEShop.Tests
{
    public class UserTests
    {
        private Mock<IDataContext> context;
        private GuestLogic guest;
        private UserLogic user;

        [SetUp]
        public void Setup()
        {
            context = new Mock<IDataContext>();

            context.Setup(x => x.UsersList).Returns(
                new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Name = "user1",
                        Email = "user1@gmail.com",
                        Password = "1111",
                        ShoppingCart = new ShoppingCart(),
                        OrderList = new List<Order>()
                    }
            });

            context.Setup(x => x.ProductsList).Returns(new List<Product>()
            {
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
            user = new UserLogic(context.Object);
            guest.LogIn(context.Object.UsersList[0].Email, context.Object.UsersList[0].Password);
        }

        [Test]
        public void ViewShoppingCart_ReturnCollection()
        {
            // arrange
            List<string> productsList;

            // act
            user.AddToShoppingCart(1);
            productsList = user.ViewShoppingCart();

            // assert
            Assert.IsNotEmpty(productsList);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void AddToShoppingCart_ReturnTrue(int productId)
        {
            // arrange
            int id = productId;
            int expectedCount = user.ViewShoppingCart().Count + 1;

            // act
            bool actualResult = user.AddToShoppingCart(id);
            int actualCount = user.ViewShoppingCart().Count;

            // assert
            Assert.AreEqual(true, actualResult);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(10000)]
        public void AddToShoppingCart_ReturnFalse(int productId)
        {
            // arrange
            int id = productId;
            int expectedCount = user.ViewShoppingCart().Count;

            // act
            bool actualResult = user.AddToShoppingCart(id);
            int actualCount = user.ViewShoppingCart().Count;

            // assert
            Assert.AreEqual(false, actualResult);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void RemoveFromCart_ReturnTrue(int productId)
        {
            // arrange
            int id = productId;

            // act
            user.AddToShoppingCart(id);
            bool actualResult = user.RemoveFromShoppingCart(id);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void RemoveFromCart_ReturnFalse(int productId)
        {
            // arrange
            int id = productId;

            // act
            bool actualResult = user.RemoveFromShoppingCart(id);

            // assert
            Assert.AreEqual(false, actualResult);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void ViewShoppingCart_ReturnCollection(int productId)
        {
            // arrange
            int id = productId;

            // act
            user.AddToShoppingCart(id);
            var actualResult = user.ViewShoppingCart();

            // assert
            Assert.IsNotEmpty(actualResult);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void CreateOrder_ReturnTrue(int productId)
        {
            // arrange
            int id = productId;

            // act
            user.AddToShoppingCart(id);
            bool actualResult = user.CreateOrder();

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase(1)]
        [TestCase(2)]
        public void ViewOrders_ReturnCollection(int productId)
        {
            // arrange
            int id = productId;

            // act
            user.AddToShoppingCart(id);
            user.CreateOrder();
            var actualResult = user.ViewOrders();

            // assert
            Assert.IsNotEmpty(actualResult);
        }

        [TestCase("Name1")]
        [TestCase("Name2")]
        public void ChangeName_ReturnTrue(string name)
        {
            // arrange
            string newName = name;

            // act
            bool actualResult = user.ChangeName(newName);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase("")]
        [TestCase("a")]
        public void ChangeName_ReturnFalse(string name)
        {
            // arrange
            string newName = name;

            // act
            bool actualResult = user.ChangeName(newName);

            // assert
            Assert.AreEqual(false, actualResult);
        }

        [TestCase("adq23kj44")]
        [TestCase("dalkjhue3jk")]
        public void ChangePassword_ReturnTrue(string password)
        {
            // arrange
            string newPassword = password;

            // act
            bool actualResult = user.ChangePassword(newPassword);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase("")]
        [TestCase("a1")]
        public void ChangePassword_ReturnFalse(string password)
        {
            // arrange
            string newPassword = password;

            // act
            bool actualResult = user.ChangePassword(newPassword);

            // assert
            Assert.AreEqual(false, actualResult);
        }

        [TestCase(1)]
        public void CancelOrder_ReturnTrue(int orderId)
        {
            // arrange
            int id = orderId;

            // act
            user.AddToShoppingCart(id);
            user.CreateOrder();
            bool actualResult = user.CancelOrder(id);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void CancelOrder_ReturnFalse(int orderId)
        {
            // arrange
            int id = orderId;

            // act
            bool actualResult = user.CancelOrder(id);

            // assert
            Assert.AreEqual(false, actualResult);
        }

        [TestCase(1)]
        public void ReceivingOrder_ReturnTrue(int orderId)
        {
            // arrange
            int id = orderId;

            // act
            user.AddToShoppingCart(id);
            user.CreateOrder();
            bool actualResult = user.ReceivingOrder(id);

            // assert
            Assert.AreEqual(true, actualResult);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void ReceivingOrder_ReturnFalse(int orderId)
        {
            // arrange
            int id = orderId;

            // act
            bool actualResult = user.ReceivingOrder(id);

            // assert
            Assert.AreEqual(false, actualResult);
        }
    }
}