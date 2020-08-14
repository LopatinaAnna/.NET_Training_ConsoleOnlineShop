using ConsoleEShop.DAL.Entities;
using ConsoleEShop.DAL.Entities.Accounts;
using System.Collections.Generic;

namespace ConsoleEShop.DAL
{
    /// <summary>
    /// Defines context interface
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// List of products
        /// </summary>
        List<Product> ProductsList { get; set; }

        /// <summary>
        /// List of users
        /// </summary>
        List<User> UsersList { get; set; }

        /// <summary>
        /// List of admins
        /// </summary>
        List<Admin> AdminsList { get; set; }
    }
}
