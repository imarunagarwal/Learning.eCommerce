using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using CartWebApi.BusinessAccessLayer.Contracts;
using CartWebApi.SharedLayer.Dtos;
using CartWebApi.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CartWebApi.WebApi.Controller
{
    /// <summary>
    /// The Cart Controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartBAL _cartBAL;
        private readonly IHttpClientFactory _clientFactory;

        public CartController(IMapper mapper, ICartBAL cartBAL, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cartBAL = cartBAL ?? throw new ArgumentNullException(nameof(cartBAL));
        }

        public async Task<IActionResult> AddItemToCart(ItemsViewModel item)
        {
            try
            {
                Guid userId = Guid.Parse(User.Identity.Name);
                var response = await _cartBAL.AddItemToCart(userId, _mapper.Map<ItemsDto>(item));
                return Ok(_mapper.Map<ItemsViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<IActionResult> ChangeItemQuantity(Guid itemId, int quantity)
        {
            try
            {
                //doubt
                var response = await _cartBAL.ChangeItemQuantity(itemId, quantity);
                return Ok(_mapper.Map<ItemsViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> GetCartIdByUserId()
        {
            try
            {
                Guid userId = Guid.Parse(User.Identity.Name);
                var response = await _cartBAL.GetCartIdByUserId(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<IActionResult> RemoveItemFromCart(ItemsViewModel item)
        {
            try
            {
                var response = await _cartBAL.RemoveItemFromCart(_mapper.Map<ItemsDto>(item));
                return Ok(_mapper.Map<ItemsViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<IActionResult> GetCartDetailsByUserId()
        {
            try
            {
                Guid userId = Guid.Parse(User.Identity.Name);
                var response = await _cartBAL.GetCartByUserId(userId);

                return Ok(_mapper.Map<CartViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<IActionResult> CheckOut()
        {
            try
            {
                //Doubt Url from config file
                string token = Request.Headers[HeaderNames.Authorization];
                Guid userId = Guid.Parse(User.Identity.Name);
                var cart = await _cartBAL.GetCartByUserId(userId);

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:4001/api/Product/checkOut");
                request.Headers.Add("Authorization", token);
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                request.Content = new StringContent(JsonConvert.SerializeObject(cart.Items));
                var client = _clientFactory.CreateClient();
                var requestResponse = await client.SendAsync(request);
                var result = false;
                if (requestResponse.IsSuccessStatusCode)
                {
                    var responseStream = await requestResponse.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(responseStream);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
