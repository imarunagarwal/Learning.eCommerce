using Microsoft.EntityFrameworkCore;
using CartWebApi.DataAccessLayer.Entities;

namespace CartWebApi.DataAccessLayer.DBContext
{
    public class CartsDBContext : DbContext
    {

        public CartsDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CartEntity> Carts { get; set; }

        public DbSet<ItemsEntity> Items { get; set; }
    }
}
