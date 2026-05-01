using E_Commerce_Management_System.Modules.ProductsModule.Dtos;
using E_Commerce_Management_System.Modules.ProductsModule.Interfaces;
using E_Commerce_Management_System.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Management_System.Modules.ProductsModule.Controllesr
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>[Admin] Get all products including inactive ones.</summary>
        [HttpGet("all")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(ApiResponse<IEnumerable<ProductResponseDto>>.Ok(products));
        }

        /// <summary>[Customer] Browse active in-stock products.</summary>
        [HttpGet]
        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> GetActive()
        {
            var products = await _productService.GetActiveProductsAsync();
            return Ok(ApiResponse<IEnumerable<ProductResponseDto>>.Ok(products));
        }

        /// <summary>[Admin | Customer] Get a single product by ID.</summary>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null)
                return NotFound(ApiResponse<ProductResponseDto>.Fail($"Product {id} not found."));

            return Ok(ApiResponse<ProductResponseDto>.Ok(product));
        }

        /// <summary>[Admin] Create a new product.</summary>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var created = await _productService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ProductResponseDto>.Ok(created, "Product created successfully."));
        }

        /// <summary>[Admin] Update an existing product.</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var updated = await _productService.UpdateProductAsync(id, dto);
            if (updated is null)
                return NotFound(ApiResponse<ProductResponseDto>.Fail($"Product {id} not found."));

            return Ok(ApiResponse<ProductResponseDto>.Ok(updated, "Product updated successfully."));
        }

        /// <summary>[Admin] Delete a product.</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<object>.Fail($"Product {id} not found."));

            return Ok(ApiResponse<object>.Ok(null!, "Product deleted successfully."));
        }
    }
}
