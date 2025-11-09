using Application.DTOs.Inventory;

namespace Application.DTOs.Product
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrice { get; set; }
        public List<InventoryResponse> Inventory { get; set; } = new();
    }
}
