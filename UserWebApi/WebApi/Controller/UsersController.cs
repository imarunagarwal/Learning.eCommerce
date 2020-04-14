using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UserWebApi.BusinessAccessLayer.Contracts;
using UserWebApi.SharedLayer.Dtos;
using UserWebApi.WebApi.ViewModels;

namespace UserWebApi.WebApi.Controller
{
    /// <summary>
    /// The user Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserBAL _userBAL;
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="mapper">Mapper configuration</param>
        /// <param name="userBAL">userBAL object</param>
        /// <param name="clientFactory">Http Configuration</param>
        public UsersController(IMapper mapper, IUserBAL userBAL, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userBAL = userBAL ?? throw new ArgumentNullException(nameof(userBAL));
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">UserViewModel</param>
        /// <returns>Returns the ResponseViewModel object</returns>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser(UserViewModel user)
        {
            try
            {
                var isUserExist = await _userBAL.IsUserExistsAsync(user.EmailId);
                if (isUserExist)
                {
                    return BadRequest("User Already Exists");
                }
                user.UserId = new Guid();
                var response = await _userBAL.CreateUserAsync(_mapper.Map<UserDto>(user));
                
                Guid cartId = await CreateCart(response.Token);
                var result = await _userBAL.AddCartId(user.UserId, cartId);
                if (!result)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Cart Id not saved in user table");
                }
                return Ok(_mapper.Map<ResponseViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="loginUser">LoginUserViewModel</param>
        /// <returns>Returns the RsponseDto object</returns>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginUserViewModel loginUser)
        {
            try
            {
                var response = await _userBAL.LoginAsync(_mapper.Map<LoginUserDto>(loginUser));

                if (response.SuccessStatus == false)
                {
                    return BadRequest(new { message = "UserName or Password is invalid" });
                }
                return Ok(_mapper.Map<ResponseViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Edit user record
        /// </summary>
        /// <param name="user">UserViewModel</param>
        /// <returns>Returns bool value to show success or failure</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] UserViewModel user)
        {
            try
            {
                bool result = await _userBAL.EditUserAsync(_mapper.Map<UserDto>(user));

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Error at backend is blocking edit.");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a user record based on token passed
        /// </summary>
        /// <returns>The UserViewModel based on token</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var currentUserId = Guid.Parse(User.Identity.Name);

                var user = await _userBAL.GetUserByIdAsync(currentUserId);

                if (user == null)
                {
                    return NotFound("User not found!");
                }

                return Ok(_mapper.Map<UserViewModel>(user));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Checks if the user Exists
        /// </summary>
        /// <returns>Boolean result based on the existance of user</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> IsUserExists()
        {
            try
            {
                var currentUserId = Guid.Parse(User.Identity.Name);

                var user = await _userBAL.GetUserByIdAsync(currentUserId);

                return Ok(user == null ? false : true);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task<Guid> CreateCart(string token)
        {
            token = string.Concat("Bearer ", token);
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:4002/api/Cart/Create");
            request.Headers.Add("Authorization", token);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            
            Guid.TryParse(await response.Content.ReadAsStringAsync(), out Guid cartId);
            
            return cartId;
        }
    }
}
