using Application.DTOs.Product;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductLocationDto>> GetAllWithLocationAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<ProductResponse> CreateAsync(Product product, int palletId, int positionNumber, int quantity);
        Task UpdatePricesAsync(int id, decimal retailPrice, decimal wholesalePrice);
        Task UpdateStockAsync(int productId, int palletId, int positionNumber, int newQuantity);
    }
}