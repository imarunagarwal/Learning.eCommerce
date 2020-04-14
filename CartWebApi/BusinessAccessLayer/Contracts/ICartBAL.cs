using CartWebApi.SharedLayer.Dtos;
using System;
using System.Threading.Tasks;

namespace CartWebApi.BusinessAccessLayer.Contracts
{
    public interface ICartBAL
    {
        Task<ItemsDto> AddItemToCart(Guid userId, ItemsDto item);

        Task<ItemsDto> ChangeItemQuantity(Guid itemId, int quantity);

        Task<Guid> GetCartIdByUserId(Guid userId);

        Task<ItemsDto> RemoveItemFromCart(ItemsDto item);

        Task<CartDto> GetCartByUserId(Guid userId);
    }
}