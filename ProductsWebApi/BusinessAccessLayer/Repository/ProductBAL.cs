using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductWebApi.BusinessAccessLayer.Contracts;
using ProductWebApi.DataAccessLayer.Contracts;
using ProductWebApi.SharedLayer.Dtos;

namespace ProductWebApi.BusinessAccessLayer.Repository
{
    public class ProductBAL : IProductBAL
    {
        private readonly IProductDAL _productDAL;
        public ProductBAL(IProductDAL productDAL)
        {
            _productDAL = productDAL ?? throw new ArgumentNullException(nameof(productDAL));
        }

        public async Task<ProductDto> AddProductAsync(ProductDto product)
        {
            try
            {
                return await _productDAL.AddProductAsync(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CheckOutCartProductsAsync(List<CartCheckoutDto> cartItemsList)
        {
            try
            {
                return await _productDAL.CheckOutCartProductsAsync(cartItemsList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> DeleteProductAsync(Guid productId)
        {
            try
            {
                return await _productDAL.DeleteProductAsync(productId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ProductDto> EditProductAsync(ProductDto product)
        {
            try
            {
                return await _productDAL.EditProductAsync(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            try
            {
                return await _productDAL.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid productId)
        {
            try
            {
                return await _productDAL.GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsAddToCartpossible(ProductDto product)
        {
            try
            {
                return await _productDAL.IsAddToCartpossible(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
