using AutoMapper;
using CartWebApi.DataAccessLayer.Contracts;
using CartWebApi.DataAccessLayer.DBContext;
using CartWebApi.DataAccessLayer.Entities;
using CartWebApi.SharedLayer.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CartWebApi.DataAccessLayer.Repository
{
    public class CartDAL : ICartDAL
    {
        private readonly IMapper _mapper;
        private readonly CartsDBContext _context;

        public CartDAL(IMapper mapper, CartsDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> GetCartIdByUserId(Guid userId)
        {
            try
            {
                var cart = await _context.Carts.Where(cart => cart.UserId == userId).FirstOrDefaultAsync();

                if(cart  == null)
                {
                    cart = new CartEntity()
                    {
                        CartId = new Guid(),
                        UserId = userId
                    };
                    await _context.Carts.AddAsync(cart);
                    await _context.SaveChangesAsync();
                }

                return cart.CartId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItemsDto> AddItemToCart(Guid userId, ItemsDto item)
        {
            try
            {
                var cart = _context.Carts.Where(cart => cart.UserId == userId).FirstOrDefault();
                if(cart == null || cart.CartId != item.CartId)
                {
                    throw new Exception("Unauthorized");
                }
                ItemsEntity itemEntity = _mapper.Map<ItemsEntity>(item);
                itemEntity.ItemId = new Guid();
                await _context.Items.AddAsync(itemEntity);
                await _context.SaveChangesAsync();

                return _mapper.Map<ItemsDto>(itemEntity);
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
                var itemEntity = _mapper.Map<ItemsEntity>(item);
                _context.Items.Remove(itemEntity);
                await _context.SaveChangesAsync();

                return _mapper.Map<ItemsDto>(item);
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
                var item = await _context.Items.FindAsync(itemId);
                item.Quantity = quantity;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _mapper.Map<ItemsDto>(item);
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
                var cart = await _context.Carts.Where(cart => cart.UserId == userId).FirstOrDefaultAsync();
                var items = cart.Items;

                return _mapper.Map<CartDto>(cart);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
