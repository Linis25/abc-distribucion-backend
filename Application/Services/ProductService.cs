using Application.DTOs.Product;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService(IProductRepository _repository) : IProductService
    {
        public async Task<List<ProductLocationDto>> GetAllWithLocationAsync()
            => await _repository.GetAllWithLocationAsync();

        public async Task<ProductResponse?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task<ProductResponse> CreateAsync(Product product, int palletId, int positionNumber, int quantity)
            => await _repository.CreateAsync(product, palletId, positionNumber, quantity);

        public async Task UpdatePricesAsync(int id, decimal retailPrice, decimal wholesalePrice)
            => await _repository.UpdatePricesAsync(id, retailPrice, wholesalePrice);

        public async Task UpdateStockAsync(int productId, int palletId, int positionNumber, int newQuantity)
            => await _repository.UpdateStockAsync(productId, palletId, positionNumber, newQuantity);
    }
}