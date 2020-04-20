using AutoMapper;
using System;
using System.Threading.Tasks;
using ProductWebApi.DataAccessLayer.DBContext;
using ProductWebApi.SharedLayer.Dtos;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.DataAccessLayer.Entities;
using ProductWebApi.DataAccessLayer.Contracts;

namespace ProductWebApi.DataAccessLayer.Repository
{
    public class ProductDAL : IProductDAL
    {
        private readonly IMapper _mapper;
        private readonly ProductsDBContext _context;

        public ProductDAL(IMapper mapper, ProductsDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> AddProductAsync(ProductDto product)
        {
            try
            {
                product.ProductId = new Guid();
                var addedProduct = await _context.Products.AddAsync(_mapper.Map<ProductEntity>(product));
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductDto>(addedProduct);
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
                var product = await _context.Products.FindAsync(productId);
                return _mapper.Map<ProductDto>(product);
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
                bool result = true;
                foreach (var item in cartItemsList)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product.Quantity - item.Quantity < 0)
                    {
                        result = false;
                        break;
                    }
                    product.Quantity -= item.Quantity;
                    _context.Entry(product).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                return result;
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
                var editedProduct = _context.Entry(_mapper.Map<ProductEntity>(product)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductDto>(editedProduct);
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
                var product = await _context.Products.FindAsync(productId);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductDto>(product);
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
                var productEntity = await _context.Products.FindAsync(product.ProductId);
                return (productEntity.Quantity >= product.Quantity) ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
