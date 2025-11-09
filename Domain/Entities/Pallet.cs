namespace Domain.Entities
{
    public class Pallet
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int TotalPositions { get; set; }
        public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
    }
}
