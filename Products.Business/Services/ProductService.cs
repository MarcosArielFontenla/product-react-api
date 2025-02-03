using Products.Business.Services.Interfaces;
using Products.Data.Models;
using Products.Data.Repositories.Interfaces;

namespace Products.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<List<Product>> GetAllProductsAsync()
        {
            return _productRepository.GetAllProducts();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public Task<Product> AddProductAsync(Product product)
        {
            return _productRepository.AddProduct(product);
        }

        public Task<Product> UpdateProductAsync(int id, Product product)
        {
            return _productRepository.UpdateProduct(id, product);
        }

        public Task<Product> DeleteProductAsync(int id)
        {
            return _productRepository.DeleteProduct(id);
        }
    }
}
