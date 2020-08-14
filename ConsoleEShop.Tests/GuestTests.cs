using ConsoleEShop.BLL;
using ConsoleEShop.DAL;
using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ConsoleEShop.Tests
{
    public class GuestTests
    {
        private Mock<IDataContext> context;
        private GuestLogic guest;

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

            context.Setup(x => x.ProductsList).Returns(new List<Product>() 
            { 
                new Product()
                {
                    Category = Category.Phones,
                    Name = "Samsung Galaxy A51",
                    Price = 300,
                    Id = 1 
                }
            });

            guest = new GuestLogic(context.Object);
        }

        [Test]
        public void LogInUserAccount_ReturnTrue()
        {
            // arrange
            string email = context.Object.UsersList[0].Email;
            string password = context.Object.UsersList[0].Password;

            // act
            bool actual = guest.LogIn(email, password);

            // assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void LogInAdminAccount_ReturnTrue()
        {
            // arrange
            string email = context.Object.AdminsList[0].Email;
            string password = context.Object.AdminsList[0].Password;

            // act
            bool actual = guest.LogIn(email, password);

            // assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void LogIn_ReturnFalse()
        {
            // arrange
            string email = "";
            string password = "";

            // act
            bool actual = guest.LogIn(email, password);

            // assert
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void RegisterNewAccount_ReturnTrue()
        {
            // arrange
            string name = "NewName";
            string email = "name@gmail.com";
            string password = "df7kdf98h";

            // act
            bool actual = guest.Register(name, email, password);

            // assert
            Assert.AreEqual(true, actual);
        }

        [Test]
        public void RegisterExistAccount_ReturnFalse()
        {
            // arrange
            string name = context.Object.UsersList[0].Name;
            string email = context.Object.UsersList[0].Email;
            string password = context.Object.UsersList[0].Password;

            // act
            bool actual = guest.Register(name, email, password);

            // assert
            Assert.AreEqual(false, actual);
        }

        [Test]
        public void ViewAllProducts_ReturnCollection()
        {
            // arrange
            List<string> productsList;

            // act
            productsList = guest.GetListOfProducts();

            // assert
            Assert.IsNotEmpty(productsList);
        }

        [Test]
        public void SearchProductbyName_ReturnCollection()
        {
            // arrange
            string nameToSearch = "Samsung";
            List<string> productsList;

            // act
            productsList = guest.SearchProductbyName(nameToSearch);

            // assert
            Assert.IsNotEmpty(productsList);
        }
    }
}