using Application.Constants;
using Application.DTOs.Inventory;
using Application.DTOs.Product;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public async Task<List<ProductLocationDto>> GetAllWithLocationAsync()
        {
            return await context.Product
                .AsNoTracking()
                .SelectMany(p => p.Inventory.Select(i => new ProductLocationDto
                {
                    Id = p.Id,
                    Sku = p.Sku,
                    Name = p.Name,
                    Description = p.Description ?? string.Empty,
                    RetailPrice = p.RetailPrice,
                    WholesalePrice = p.WholesalePrice,
                    Pallet = i.Pallet != null ? i.Pallet.Code : string.Empty,
                    PositionNumber = i.PositionNumber,
                    Quantity = i.Quantity
                }))
                .ToListAsync();
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            return await context.Product
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Sku = p.Sku,
                    Name = p.Name,
                    Description = p.Description,
                    RetailPrice = p.RetailPrice,
                    WholesalePrice = p.WholesalePrice,
                    Inventory = p.Inventory.Select(i => new InventoryResponse
                    {
                        PalletId = i.PalletId,
                        Pallet = i.Pallet != null ? i.Pallet.Code : string.Empty,
                        PositionNumber = i.PositionNumber,
                        Quantity = i.Quantity
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductResponse> CreateAsync(Product product, int palletId, int positionNumber, int quantity)
        {
            if (await context.Product.AnyAsync(p => p.Sku == product.Sku))
                throw new InvalidOperationException(ErrorMessages.ProductExists);

            if (product.RetailPrice <= 0 || product.WholesalePrice <= 0 || quantity <= 0)
                throw new ArgumentException(ErrorMessages.InvalidQuantityOrPrice);

            await EnsurePalletExistsAsync(palletId);

            bool positionOccupied = await context.Inventory
                .AnyAsync(i => i.PalletId == palletId && i.PositionNumber == positionNumber && i.Quantity > 0);

            if (positionOccupied)
                throw new InvalidOperationException(ErrorMessages.PositionOccupied);

            await context.Product.AddAsync(product);
            context.Inventory.Add(new Inventory
            {
                Product = product,
                PalletId = palletId,
                PositionNumber = positionNumber,
                Quantity = quantity
            });

            await context.SaveChangesAsync();

            return new ProductResponse
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                RetailPrice = product.RetailPrice,
                WholesalePrice = product.WholesalePrice,
                Inventory = new List<InventoryResponse>
                {
                    new InventoryResponse
                    {
                        PalletId = palletId,
                        Pallet = string.Empty,
                        PositionNumber = positionNumber,
                        Quantity = quantity
                    }
                }
            };
        }

        public async Task UpdatePricesAsync(int productId, decimal retailPrice, decimal wholesalePrice)
        {
            if (retailPrice <= 0 || wholesalePrice <= 0)
                throw new ArgumentException(ErrorMessages.InvalidPrice);

            var product = await context.Product.FirstOrDefaultAsync(p => p.Id == productId);
            if (product is null)
                throw new InvalidOperationException(ErrorMessages.ProductNotFound);

            product.RetailPrice = retailPrice;
            product.WholesalePrice = wholesalePrice;

            await context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(int productId, int palletId, int positionNumber, int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException(ErrorMessages.NegativeQuantity);

            await EnsureProductExistsAsync(productId);
            await EnsurePalletExistsAsync(palletId);

            var inventory = await context.Inventory
                .FirstOrDefaultAsync(i => i.ProductId == productId)
                ?? throw new KeyNotFoundException(ErrorMessages.InventoryNotFound);

            bool positionOccupied = await context.Inventory.AnyAsync(i =>
                i.PalletId == palletId &&
                i.PositionNumber == positionNumber &&
                i.ProductId != productId &&
                i.Quantity > 0);

            if (positionOccupied)
                throw new InvalidOperationException(ErrorMessages.PositionOccupied);

            inventory.PalletId = palletId;
            inventory.PositionNumber = positionNumber;
            inventory.Quantity = newQuantity;

            await context.SaveChangesAsync();
        }

        private async Task EnsureProductExistsAsync(int productId)
        {
            if (!await context.Product.AnyAsync(p => p.Id == productId))
                throw new KeyNotFoundException(ErrorMessages.ProductNotFound);
        }

        private async Task EnsurePalletExistsAsync(int palletId)
        {
            if (!await context.Pallet.AnyAsync(p => p.Id == palletId))
                throw new KeyNotFoundException(ErrorMessages.PalletNotFound);
        }
    }
}