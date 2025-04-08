using MapsterMapper;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using OnlineOrderManagementSystem.Repository.IRepository.Sal;
using OnlineOrderManagementSystem.Repository.IServices.Sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Inferastructure.Services.Sal
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepoistory productRepoistory;
        private readonly IMapper mapper;

        public ProductService(IProductRepoistory productRepoistory, IMapper mapper)
        {
            this.productRepoistory = productRepoistory;
            this.mapper = mapper;
        }

        public async Task<bool> AddProductAsync(AddProductDTO product)
        {
            var newProduct = mapper.Map<Product>(product);
            return await productRepoistory.AddProductAsync(newProduct);
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            return await productRepoistory.DeleteProductAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(GetProductsDTO dto)
        {
            return await productRepoistory.GetAllProductsAsync(dto);
        }

        public async Task<Product?> GetProductByIdAsync(long productId)
        {
            return await productRepoistory.GetProductByIdAsync(productId);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await productRepoistory.UpdateProductAsync(product);
        }
    }
}
