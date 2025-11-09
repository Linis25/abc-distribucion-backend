namespace Application.DTOs.Product
{
    public class ProductLocationDto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrice { get; set; }
        public string Pallet { get; set; } = string.Empty;
        public int PositionNumber { get; set; }
        public int Quantity { get; set; }
    }

}
