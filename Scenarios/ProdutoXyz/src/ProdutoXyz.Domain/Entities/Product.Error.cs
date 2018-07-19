namespace ProdutoXyz.Domain.Entities
{
    public partial class Product
    {
        public enum Error
        {
            ProductShouldHaveDescription,
            ProductShouldHaveValue
        }
    }
}
