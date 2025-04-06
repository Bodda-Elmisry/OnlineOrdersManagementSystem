using Microsoft.EntityFrameworkCore;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Inferastructure.Data;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Repositories.Sal
{
    internal class ProductRepoistory : IProductRepoistory
    {
        private readonly AppDbContext context;

        public ProductRepoistory(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            context.Products.Add(product);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null)
            {
                return false;
            }
            context.Products.Remove(product);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(GetProductsDTO dto)
        {
            var query = context.Products.AsQueryable();

            query = dto.name != null ? query.Where(p => p.Name.Contains(dto.name)) : query;
            query = dto.description != null ? query.Where(p => p.Description.Contains(dto.description)) : query;
            query = dto.minPrice != null ? query.Where(p=> p.Price >= dto.minPrice) : query;
            query = dto.maxPrice != null ? query.Where(p=> p.Price <= dto.maxPrice) : query;
            query = dto.minQuantity != null ? query.Where(p => p.StockQuantity >= dto.minQuantity) : query;
            query = dto.maxQuantity != null ? query.Where(p => p.StockQuantity <= dto.maxQuantity) : query;

            return await query.OrderBy(p=> p.Name).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await context.Products.FirstOrDefaultAsync(p=> p.Id == productId);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            context.Products.Update(product);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
