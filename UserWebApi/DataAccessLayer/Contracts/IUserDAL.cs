using System;
using System.Threading.Tasks;
using UserWebApi.SharedLayer.Dtos;

namespace UserWebApi.DataAccessLayer.Contracts
{
    public interface IUserDAL
    {
        Task<ResponseDto> CreateUserAsync(UserDto user);

        Task<bool> EditUserAsync(UserDto user);

        Task<UserDto> GetUserByIdAsync(Guid userId);

        Task<bool> IsUserExistsAsync(string emailId);

        Task<ResponseDto> LoginAsync(LoginUserDto loginUser);
    }
}