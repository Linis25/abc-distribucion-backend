namespace Application.DTOs.Inventory
{
    public class InventoryResponse
    {
        public int PalletId { get; set; }
        public string Pallet { get; set; } = string.Empty;
        public int PositionNumber { get; set; }
        public int Quantity { get; set; }
    }
}
