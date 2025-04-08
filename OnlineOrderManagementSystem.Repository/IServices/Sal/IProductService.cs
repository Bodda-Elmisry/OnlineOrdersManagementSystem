﻿using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Repository.IServices.Sal
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(AddProductDTO product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(long productId);
        Task<Product?> GetProductByIdAsync(long productId);
        Task<IEnumerable<Product>> GetAllProductsAsync(GetProductsDTO dto);
    }
}
