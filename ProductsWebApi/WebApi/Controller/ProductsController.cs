using AutoMapper;
using Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using ProductWebApi.BusinessAccessLayer.Contracts;
using ProductWebApi.SharedLayer.Dtos;
using ProductWebApi.WebApi.ViewModels;
using System.Collections.Generic;

namespace ProductWebApi.WebApi.Controller
{
    /// <summary>
    /// The user Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductBAL _productBAL;

        public ProductsController(IMapper mapper, IProductBAL userBAL)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _productBAL = userBAL ?? throw new ArgumentNullException(nameof(userBAL));
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [HttpPost("Create")]
        public async Task<IActionResult> AddProduct(ProductViewModel product)
        {
            try
            {
                var response = await _productBAL.AddProductAsync(_mapper.Map<ProductDto>(product));
                return Ok(_mapper.Map<ProductViewModel>(response));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("Edit")]
        public async Task<IActionResult> EditProduct([FromBody] ProductViewModel product)
        {
            try
            {
                var result = await _productBAL.EditProductAsync(_mapper.Map<ProductDto>(product));

                if (result != null)
                {
                    return Ok(_mapper.Map<ProductViewModel>(result));
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

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productBAL.GetAllProductsAsync();

                return Ok(_mapper.Map<List<ProductViewModel>>(products));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromBody] Guid productId)
        {
            try
            {
                var product = await _productBAL.GetProductByIdAsync(productId);

                return Ok(_mapper.Map<ProductViewModel>(product));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
