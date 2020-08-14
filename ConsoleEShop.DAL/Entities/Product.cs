namespace ConsoleEShop.DAL.Entities
{
    /// <summary>
    /// Define product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Product category
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product id
        /// </summary>
        public int Id { get; set; }
    }
}
