using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIDAsync(int id);
        Task<IReadOnlyList<Product>> GetProductListAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandListAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypeListAsync();
    }
}