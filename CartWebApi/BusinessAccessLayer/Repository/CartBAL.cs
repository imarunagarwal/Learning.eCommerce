using CartWebApi.BusinessAccessLayer.Contracts;
using CartWebApi.DataAccessLayer.Contracts;
using CartWebApi.SharedLayer.Dtos;
using System;
using System.Threading.Tasks;

namespace CartWebApi.BusinessAccessLayer.Repository
{
    public class CartBAL : ICartBAL
    {
        private readonly ICartDAL _cartDAL;

        public CartBAL(ICartDAL cartDAL)
        {
            _cartDAL = cartDAL ?? throw new ArgumentNullException(nameof(cartDAL));
        }

        public async Task<ItemsDto> AddItemToCart(Guid userId, ItemsDto item)
        {
            try
            {
                return await _cartDAL.AddItemToCart(userId, item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItemsDto> ChangeItemQuantity(Guid itemId, int quantity)
        {
            try
            {
                return await _cartDAL.ChangeItemQuantity(itemId, quantity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid> GetCartIdByUserId(Guid userId)
        {
            try
            {
                return await _cartDAL.GetCartIdByUserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItemsDto> RemoveItemFromCart(ItemsDto item)
        {
            try
            {
                return await _cartDAL.RemoveItemFromCart(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CartDto> GetCartByUserId(Guid userId)
        {
            try
            {
                return await _cartDAL.GetCartByUserId(userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
