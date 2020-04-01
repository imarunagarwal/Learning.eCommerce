using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using UserWebApi.DataAccessLayer.Entities;
using static UserWebApi.SharedLayer.Enums.Enums;

namespace UserWebApi.DataAccessLayer.DBContext
{
    public class UsersDBContext : DbContext
    {

        public UsersDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        private string GenerateHashedPassword(string password)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "admin",
                    LastName = "user",
                    EmailId = "admin.user@testing.com",
                    Password = GenerateHashedPassword("Arun@1234"),
                    Gender = Gender.Male,
                    Age = 1,
                    PhoneNo = "1234512345"
                }
            );
        }
    }
}
