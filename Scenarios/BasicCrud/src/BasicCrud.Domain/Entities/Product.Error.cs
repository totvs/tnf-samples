namespace BasicCrud.Domain.Entities
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
