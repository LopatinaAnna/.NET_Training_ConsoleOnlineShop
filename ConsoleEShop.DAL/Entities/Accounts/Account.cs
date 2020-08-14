using System.Collections.Generic;

namespace ConsoleEShop.DAL.Entities.Accounts
{
    /// <summary>
    /// Defines common fields for user and admin 
    /// </summary>
    public abstract class Account
    {
        /// <summary>
        /// Defines account id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Defines account name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines account email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Defines account password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Defines account shopping cart
        /// </summary>
        public ShoppingCart ShoppingCart { get; set; }

        /// <summary>
        /// Defines account order list
        /// </summary>
        public List<Order> OrderList { get; set; }
    }
}