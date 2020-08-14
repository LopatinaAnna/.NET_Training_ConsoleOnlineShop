using ConsoleEShop.DAL;
using ConsoleEShop.DAL.Entities.Accounts;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEShop.BLL
{
    /// <summary>
    /// Defines basic functionality
    /// </summary>
    public abstract class BaseLogic
    {
        /// <summary>
        /// Defines context
        /// </summary>
        protected static IDataContext Context { get; set; }

        /// <summary>
        /// Defines account
        /// </summary>
        protected static Account Account { get; set; }

        /// <summary>
        /// Defines a new instance of the class and sets context
        /// </summary>
        protected BaseLogic()
        {
            Context = new DataContext();
        }

        /// <summary>
        /// Defines a new instance of the class and sets context by parameter
        /// </summary>
        /// <param name="context"></param>
        protected BaseLogic(IDataContext context)
        {
            Context = context;
        }

        /// <summary>
        /// View the list of products
        /// </summary>
        /// <returns>New object which is list of products</returns>
        public List<string> GetListOfProducts()
        {
            List<string> products = new List<string>();

            var getProducts = Context.ProductsList.GroupBy(x => x.Category);

            if (getProducts is null)
                return products;

            foreach (var category in getProducts)
            {
                products.Add(category.Key.ToString());
                foreach (var product in category)
                {
                    products.Add($"   {product.Name} - price: {product.Price}$ (ID: {product.Id})");
                }
                products.Add("");
            }

            return products;
        }

        /// <summary>
        /// Search for a product by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>New object which is list of products</returns>
        public List<string> SearchProductbyName(string name)
        {
            List<string> products = new List<string>();

            var getProducts = Context.ProductsList.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();

            if (getProducts is null)
                return products;

            foreach (var product in getProducts)
            {
                products.Add($"   {product.Name} - price: {product.Price}$ (ID: {product.Id})");
            }

            return products;
        }
    }
}
