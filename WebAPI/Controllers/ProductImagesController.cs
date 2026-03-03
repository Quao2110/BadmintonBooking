using Application.DTOs.ApiResponseDTO;
using Application.DTOs.RequestDTOs.ProductImage;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/product-images")]
[ApiController]
public class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _productImageService;

    public ProductImagesController(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? productId)
    {
        try
        {
            if (productId.HasValue)
            {
                var result = await _productImageService.GetByProductIdAsync(productId.Value);
                return Ok(ApiResponse.Success("Product images retrieved successfully.", result));
            }

            var all = await _productImageService.GetAllAsync();
            return Ok(ApiResponse.Success("Product images retrieved successfully.", all));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var result = await _productImageService.GetByIdAsync(id);
            return Ok(ApiResponse.Success("Product image retrieved successfully.", result));
        }
        catch (Exception ex)
        {
            return NotFound(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductImageCreateRequest request)
    {
        try
        {
            var result = await _productImageService.CreateAsync(request);
            return Ok(ApiResponse.Success("Product image created successfully.", result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductImageUpdateRequest request)
    {
        try
        {
            var result = await _productImageService.UpdateAsync(id, request);
            return Ok(ApiResponse.Success("Product image updated successfully.", result));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _productImageService.DeleteAsync(id);
            return Ok(ApiResponse.Success("Product image deleted successfully."));
        }
        catch (Exception ex)
        {
            return NotFound(ApiResponse.Fail(ex.Message));
        }
    }
}
