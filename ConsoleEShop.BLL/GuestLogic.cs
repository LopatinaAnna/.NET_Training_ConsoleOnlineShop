using ConsoleEShop.DAL;
using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using System.Collections.Generic;

namespace ConsoleEShop.BLL
{
    /// <summary>
    /// Defines guest functionality
    /// </summary>
    public class GuestLogic : BaseLogic
    {
        /// <summary>
        /// Defines a new instance of the class
        /// </summary>
        public GuestLogic() : base()
        {
        }

        /// <summary>
        /// Defines a new instance of the class and sets context by parameter
        /// </summary>
        /// <param name="context"></param>
        public GuestLogic(IDataContext context) : base(context)
        {
        }

        /// <summary>
        /// Defines unique user id
        /// </summary>
        private int UserId => Context.UsersList.Count + 1;

        /// <summary>
        /// Defines log in
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>True if login was passed succesfully, false if user wasn't found</returns>
        public bool LogIn(string email, string password)
        {
            Account logInAccount = Context.UsersList.Find(x => x.Email == email && x.Password == password);

            if (logInAccount is null)
                logInAccount = Context.AdminsList.Find(x => x.Email == email && x.Password == password);

            if (logInAccount is null)
                return false;

            Account = logInAccount;
            return true;
        }

        /// <summary>
        /// Defines register
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>True if user was new, false if user was found in user list</returns>
        public bool Register(string name, string email, string password)
        {
            User newUser = Context.UsersList.Find(x => x.Email == email);

            if (newUser != null)
                return false;

            newUser = new User()
            {
                Name = name,
                Email = email,
                Password = password,
                Id = UserId,
                ShoppingCart = new ShoppingCart(),
                OrderList = new List<Order>()
            };
            Context.UsersList.Add(newUser);
            Account = newUser;

            return true;
        }
    }
}
