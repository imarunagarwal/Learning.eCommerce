using Microsoft.EntityFrameworkCore;
using ProductWebApi.DataAccessLayer.Entities;

namespace ProductWebApi.DataAccessLayer.DBContext
{
    public class ProductsDBContext : DbContext
    {

        public ProductsDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
