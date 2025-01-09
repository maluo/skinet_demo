using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        // private IProductRepository _repo;
        // private readonly StoreContext _context;
        // public ProductsController(StoreContext context,IProductRepository repo){
        //     _context = context;
        //     _repo = repo;
        // }
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typessRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductType> typesRepos,
            IMapper mapper){
            _productRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _typessRepo = typesRepos;
            _mapper = mapper;
        }

        [HttpGet]
        //public async Task<ActionResult<IReadOnlyList<ProductToReturn>>> GetProducts(string sort
        //    ,int? brandId, int? typeId)
        public async Task<ActionResult<Pagination<ProductToReturn>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            // var products = await _context.Products.ToListAsync();
            
            //Repository pattern
            // var products = await _repo.GetProductListAsync();
            
            //Eager loading
            //var products = await _context.Products
            //.Include(p => p.Product_type)
            //.Include(p => p.Product_brand)
            //.ToListAsync();
            //return products;

            // var products = await _productRepo.ListAllAsync();

            var spec = new ProductsWIthTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            var totalItems =  await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturn>>(products);
            //return products.Select(product => new ProductToReturn {
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    Product_brand = product.Product_brand.Name,
            //    Product_type = product.Product_type.Name
            //}).ToList();
            //return Ok(_mapper
            //    .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturn>>(products));

            return Ok(new Pagination<ProductToReturn>(productParams.PageIndex, productParams.PageSize,
                totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturn>> GetProduct(int id){
            // await _context.Products.FindAsync(id);
            //return await _repo.GetProductByIDAsync(id);
            // var product = await _context.Products
            //.Include(p => p.Product_type)
            //.Include(p => p.Product_brand)
            //.FirstOrDefaultAsync(p => p.Id == id);
            //return product;

            // var product = await _productRepo.GetTaskByIdAsync(id);
            var spec = new ProductsWIthTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            //return new ProductToReturn {
            //    Id = product.Id,
            //  Name = product.Name,
            //    Description = product.Description,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    Product_brand = product.Product_brand.Name,
            //    Product_type = product.Product_type.Name
            //};

            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product,ProductToReturn>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            // var products = await _context.Products.ToListAsync();
            //var brands = await _repo.GetProductBrandListAsync();
            var brands = await _brandsRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            // var products = await _context.Products.ToListAsync();
            //var types = await _repo.GetProductTypeListAsync();
            var types = await _typessRepo.ListAllAsync();
            return Ok(types);
        }
    }
}