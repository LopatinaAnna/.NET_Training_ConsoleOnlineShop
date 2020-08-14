using System.Collections.Generic;

namespace ConsoleEShop.DAL.Entities
{
    /// <summary>
    /// Defines user shopping cart
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// List of products in shopping cart
        /// </summary>
        public List<Product> ProductsInCart { get; set; } = new List<Product>();

        /// <summary>
        /// Returns list of products in shopping cart
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProductsInCart()
        {
            return ProductsInCart;
        }
    }
}
