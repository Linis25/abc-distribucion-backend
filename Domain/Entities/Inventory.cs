namespace Domain.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PalletId { get; set; }
        public int PositionNumber { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
        public Pallet? Pallet { get; set; }
    }
}