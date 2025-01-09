namespace Core.Entities
{
    public class Product : BaseEntity
    {
        // public int Id { get; set; }//by default would use this one as the primary key
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType Product_type { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand Product_brand { get; set; }
        public int ProductBrandId { get; set; }
    }
}