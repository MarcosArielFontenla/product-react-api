using Microsoft.EntityFrameworkCore;
using Products.Data.Context;
using Products.Data.Models;
using Products.Data.Repositories.Interfaces;

namespace Products.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var products = await _dbContext.Products.AsNoTracking().ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception("No products found", ex);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var productById = await _dbContext.Products.AsNoTracking()
                                                           .FirstOrDefaultAsync(p => p.Id == id);

                return productById;
            }
            catch (Exception ex)
            {
                throw new Exception($"Product with id: {id} not found", ex);
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                var newProduct = await _dbContext.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return newProduct.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Product was not created!", ex);
            }
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            try
            {
                var productById = await GetProductById(id);

                if (productById is not null)
                {
                    productById.Name = product.Name;
                    productById.Description = product.Description;
                    productById.Price = product.Price;
                    productById.IsAvailable = product.IsAvailable;

                    _dbContext.Update(productById);
                    await _dbContext.SaveChangesAsync();

                    return productById;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Product with id: {id} was not updated", ex);
            }
        }

        public async Task<Product> DeleteProduct(int id)
        {
            try
            {
                var productById = await GetProductById(id);

                if (productById is not null) 
                {
                    _dbContext.Remove(productById);
                    await _dbContext.SaveChangesAsync();
                    return productById;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Product with id: {id} was not found", ex);
            }
        }
    }
}
