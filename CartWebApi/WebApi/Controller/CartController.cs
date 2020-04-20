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

        private async Task<bool> IsaddItemToCartPossible(ItemsViewModel item, string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:4001/api/Product/IsAddItemToCartPossible");
                request.Headers.Add("Authorization", token);
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                request.Content = new StringContent(JsonConvert.SerializeObject(item));
                var client = _clientFactory.CreateClient();
                var requestResponse = await client.SendAsync(request);

                if (requestResponse.IsSuccessStatusCode)
                {
                    var responseStream = await requestResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(responseStream);
                }
                throw new Exception("Error Occured while sending the request");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return false;
            }
        }

        [HttpPost("Items")]
        public async Task<IActionResult> AddItemToCart(ItemsViewModel item)
        {
            try
            {
                string token = Request.Headers[HeaderNames.Authorization];
                Guid userId = Guid.Parse(User.Identity.Name);
                bool result = await IsaddItemToCartPossible(item, token);
                if (result)
                {
                    var response = await _cartBAL.AddItemToCart(userId, _mapper.Map<ItemsDto>(item));
                    return Ok(_mapper.Map<ItemsViewModel>(response));
                }
                return StatusCode((int)HttpStatusCode.Forbidden, "Not possible as the quantity in inventory is less than the required");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("Items")]
        public async Task<IActionResult> ChangeItemQuantity(Guid itemId, int quantity)
        {
            try
            {
                ItemsViewModel item = new ItemsViewModel
                {
                    Quantity = quantity,
                    ItemId = itemId
                };

                string token = Request.Headers[HeaderNames.Authorization];
                Guid userId = Guid.Parse(User.Identity.Name);
                bool result = await IsaddItemToCartPossible(item, token);
                if (result)
                {
                    var response = await _cartBAL.ChangeItemQuantity(itemId, quantity);
                    return Ok(_mapper.Map<ItemsViewModel>(response));
                }
                return StatusCode((int)HttpStatusCode.Forbidden, "Not possible as the quantity in inventory is less than the required");
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

        [HttpDelete("Items")]
        public async Task<IActionResult> RemoveItemFromCart(Guid itemId)
        {
            try
            {
                var response = await _cartBAL.RemoveItemFromCart(itemId);
                return Ok(_mapper.Map<ItemsViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartDetailsByUserId()
        {
            try
            {
                //Doubt returning only productid and quantity is sufficient or we have to fetch the products as well?
                //Doubt when user added items it was possible then the items sold out. This case is left to be handled.
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

        [HttpGet("CheckOut")]
        public async Task<IActionResult> CheckOut()
        {
            try
            {
                //Doubt Url from config file
                string token = Request.Headers[HeaderNames.Authorization];
                Guid userId = Guid.Parse(User.Identity.Name);
                var cart = await _cartBAL.GetCartByUserId(userId);

                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:4001/api/Product/CheckOut");
                request.Headers.Add("Authorization", token);
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                request.Content = new StringContent(JsonConvert.SerializeObject(cart.Items));
                var client = _clientFactory.CreateClient();
                var requestResponse = await client.SendAsync(request);

                if (requestResponse.IsSuccessStatusCode)
                {
                    var responseStream = await requestResponse.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(responseStream);

                    if (result)
                    {
                        _cartBAL.Checkout(cart.CartId);
                    }
                }

                throw new Exception("Operation not possible checkout from cart failed");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
