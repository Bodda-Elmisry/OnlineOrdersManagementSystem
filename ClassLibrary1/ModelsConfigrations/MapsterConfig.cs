using Mapster;
using OnlineOrderManagementSystem.Domain.DTOs.Cust;
using OnlineOrderManagementSystem.Domain.DTOs.Sel;
using OnlineOrderManagementSystem.Domain.Models.Cust;
using OnlineOrderManagementSystem.Domain.Models.sal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagementSystem.Domain.ModelsConfigrations
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            // Map Order to OrderResultDTO
            TypeAdapterConfig<Order, OrderResultDTO>
                .NewConfig()
                .Map(dest => dest.CustomerName, src => src.Customer.Name) // Map nested property
                .Map(dest => dest.Status, src => src.Status.ToString());  // Map enum to string

            TypeAdapterConfig<OrderItem, OrderItemResultDTO>
                .NewConfig()
                .Map(dest => dest.ProductName, src => src.Product.Name) // Map nested property
                .Map(dest => dest.OrderDate, src => src.Order.OrderDate); // Map nested property

            TypeAdapterConfig<OrderStatusHistory, OrderStatusHistoryResultDTO>
                .NewConfig()
                .Map(dest => dest.OrderDate, src => src.Order.OrderDate) // Map nested property
                .Map(dest => dest.Status, src => src.Status.ToString());  // Map enum to string

            TypeAdapterConfig<AddCustomerDTO, Customer>
                .NewConfig();

            TypeAdapterConfig<AddProductDTO, Product>
                .NewConfig();
        }
    }
}
