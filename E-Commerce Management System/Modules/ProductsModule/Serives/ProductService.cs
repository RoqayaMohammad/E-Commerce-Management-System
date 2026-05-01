using AutoMapper;
using E_Commerce_Management_System.Modules.ProductsModule.Dtos;
using E_Commerce_Management_System.Modules.ProductsModule.Interfaces;
using E_Commerce_Management_System.Shared.Entities;

namespace E_Commerce_Management_System.Modules.ProductsModule.Serives
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetActiveProductsAsync()
        {
            var products = await _productRepository.GetActiveProductsAsync();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product is null ? null : _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            var created = await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductResponseDto>(created);
        }

        public async Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return null;

            _mapper.Map(dto, product);
            product.UpdatedAt = DateTime.UtcNow;

            var updated = await _productRepository.UpdateAsync(product);
            return _mapper.Map<ProductResponseDto>(updated);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return false;

            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}
