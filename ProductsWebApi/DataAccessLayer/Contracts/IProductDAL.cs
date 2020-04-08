using ProductWebApi.SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebApi.DataAccessLayer.Contracts
{
    public interface IProductDAL
    {
        Task<ProductDto> AddProductAsync(ProductDto product);

        Task<bool> CheckOutCartProductsAsync(List<CartCheckoutDto> cartItemsList);
        
        Task<ProductDto> DeleteProductAsync(Guid productId);
        
        Task<ProductDto> EditProductAsync(ProductDto product);
        
        Task<List<ProductDto>> GetAllProductsAsync();
        
        Task<ProductDto> GetProductByIdAsync(Guid productId);
    }
}