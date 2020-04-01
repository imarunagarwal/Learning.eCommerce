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
            return await _userDAL.CreateUserAsync(user);
        }

        public async Task<bool> EditUserAsync(UserDto user)
        {
            return await _userDAL.EditUserAsync(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            return await _userDAL.GetUserByIdAsync(userId);
        }

        public async Task<bool> IsUserExistsAsync(string emailId)
        {
            return await _userDAL.IsUserExistsAsync(emailId);
        }

        public async Task<ResponseDto> LoginAsync(LoginUserDto loginUser)
        {
            return await _userDAL.LoginAsync(loginUser);
        }
    }
}
