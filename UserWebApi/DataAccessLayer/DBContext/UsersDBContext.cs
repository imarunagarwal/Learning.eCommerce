using Microsoft.EntityFrameworkCore;
using System;
using UserWebApi.DataAccessLayer.Entities;
using static UserWebApi.SharedLayer.Enums.Enums;
using UserWebApi.DataAccessLayer.Contracts;

namespace UserWebApi.DataAccessLayer.DBContext
{
    public class UsersDBContext : DbContext
    {
        private readonly ICoreRepository _coreRepository;

        public UsersDBContext(DbContextOptions options, ICoreRepository coreRepository) : base(options)
        {
            _coreRepository = coreRepository ?? throw new ArgumentNullException(nameof(coreRepository));
        }

        public DbSet<UserEntity> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "admin",
                    LastName = "user",
                    EmailId = "admin.user@testing.com",
                    Password = _coreRepository.GenerateHashedPassword("Arun@1234"),
                    Gender = Gender.Male,
                    Age = 1,
                    PhoneNo = "1234512345"
                }
            );
        }
    }
}
