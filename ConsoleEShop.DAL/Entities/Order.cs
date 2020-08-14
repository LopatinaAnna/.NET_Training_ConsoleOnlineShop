using System.Collections.Generic;

namespace ConsoleEShop.DAL.Entities
{
    /// <summary>
    /// Defines order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// List of products in order
        /// </summary>
        public List<Product> Products { get; set; }
    }
}
