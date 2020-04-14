using System;
using System.Threading.Tasks;
using UserWebApi.BusinessAccessLayer.Contracts;
using UserWebApi.DataAccessLayer.Contracts;
using UserWebApi.SharedLayer.Dtos;

namespace UserWebApi.BusinessAccessLayer.Repository
{
    public class UserBAL : IUserBAL
    {
        private readonly IUserDAL _userDAL;
        public UserBAL(IUserDAL userDAL)
        {
            _userDAL = userDAL ?? throw new ArgumentNullException(nameof(userDAL));
        }

        public async Task<ResponseDto> CreateUserAsync(UserDto user)
        {
            try
            {
                return await _userDAL.CreateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> EditUserAsync(UserDto user)
        {
            try
            {
                return await _userDAL.EditUserAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            try
            {
                return await _userDAL.GetUserByIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsUserExistsAsync(string emailId)
        {
            try
            {
                return await _userDAL.IsUserExistsAsync(emailId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseDto> LoginAsync(LoginUserDto loginUser)
        {
            try
            {
                return await _userDAL.LoginAsync(loginUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddCartId(Guid userId, Guid cartId)
        {
            try
            {
                return await _userDAL.AddCartId(userId, cartId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
