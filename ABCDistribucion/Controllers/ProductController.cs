using Application.Constants;
using Application.DTOs.Product;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABCDistribucion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController(IProductService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllWithLocation()
        {
            var products = await _service.GetAllWithLocationAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result is null)
                    return NotFound(new { message = ErrorMessages.ProductNotFound });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            try
            {
                var product = new Product
                {
                    Sku = request.Sku,
                    Name = request.Name,
                    Description = request.Description,
                    RetailPrice = request.RetailPrice,
                    WholesalePrice = request.WholesalePrice
                };

                var created = await _service.CreateAsync(product, request.PalletId, request.PositionNumber, request.Quantity);

                return CreatedAtAction(nameof(GetById), new { id = created.Id },
                    new { message = ErrorMessages.ProductCreated, productId = created.Id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("{id:int}/prices")]
        public async Task<IActionResult> UpdatePrices(int id, [FromBody] UpdatePricesRequest request)
        {
            try
            {
                await _service.UpdatePricesAsync(id, request.RetailPrice, request.WholesalePrice);
                return Ok(new { message = ErrorMessages.ProductUpdated });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("{productId:int}/stock")]
        public async Task<IActionResult> UpdateStock(int productId, [FromBody] UpdateStockRequest request)
        {
            try
            {
                await _service.UpdateStockAsync(productId, request.palletId, request.positionNumber, request.NewQuantity);
                return Ok(new { message = ErrorMessages.ProductStockUpdated });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}