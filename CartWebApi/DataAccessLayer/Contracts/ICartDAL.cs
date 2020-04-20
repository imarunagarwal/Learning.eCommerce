using CartWebApi.SharedLayer.Dtos;
using System;
using System.Threading.Tasks;

namespace CartWebApi.DataAccessLayer.Contracts
{
    public interface ICartDAL
    {
        Task<ItemsDto> AddItemToCart(Guid userId, ItemsDto item);
        
        Task<ItemsDto> ChangeItemQuantity(Guid itemId, int quantity);
        
        Task<Guid> GetCartIdByUserId(Guid userId);
        
        Task<ItemsDto> RemoveItemFromCart(Guid itemId);

        Task<CartDto> GetCartByUserId(Guid userId);

        void Checkout(Guid cartId);
    }
}