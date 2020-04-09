using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserWebApi.DataAccessLayer.Contracts;
using UserWebApi.DataAccessLayer.DBContext;
using UserWebApi.DataAccessLayer.Entities;
using UserWebApi.SharedLayer.Dtos;
using UserWebApi.SharedLayer.Helpers;

namespace UserWebApi.DataAccessLayer.Repository
{
    public class UserDAL : IUserDAL
    {
        private readonly ICoreRepository _coreRepository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly UsersDBContext _context;

        public UserDAL(IMapper mapper, UsersDBContext context, ICoreRepository coreRepository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings.Value));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _coreRepository = coreRepository ?? throw new ArgumentNullException(nameof(coreRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private ResponseDto CreateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ResponseDto
            {
                SuccessStatus = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

        public async Task<bool> IsUserExistsAsync(string emailId)
        {
            try
            {
                var result = false;
                var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.EmailId == emailId);
                if (existingUser != null)
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseDto> CreateUserAsync(UserDto user)
        {
            try
            {
                var createUser = _mapper.Map<UserEntity>(user);
                createUser.Password = _coreRepository.GenerateHashedPassword(createUser.Password);
                createUser.UserId = new Guid();
                _context.Users.Add(createUser);
                await _context.SaveChangesAsync();
                UserDto savedUser = _mapper.Map<UserDto>(createUser);
                return CreateToken(savedUser);
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
                return _mapper.Map<UserDto>(await _context.Users.FindAsync(userId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseDto> LoginAsync(LoginUserDto loginUser)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                var authenticatedUser = await _context.Users.FirstOrDefaultAsync(user => user.EmailId == loginUser.EmailId && user.Password == _coreRepository.GenerateHashedPassword(loginUser.Password));
                return CreateToken(_mapper.Map<UserDto>(authenticatedUser));
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
                var editedUser = _context.Entry(_mapper.Map<UserEntity>(user)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
