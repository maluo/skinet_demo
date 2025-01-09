using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWIthTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWIthTypesAndBrandsSpecification(ProductSpecParams productParams)
        :base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(x => x.Product_type);
            AddInclude(x => x.Product_brand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.sort)) {
                switch (productParams.sort) {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default: 
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductsWIthTypesAndBrandsSpecification(int id):base(x => x.Id == id){//x is a product type here
            AddInclude(x => x.Product_type);
            AddInclude(x => x.Product_brand);
        }
    }
}