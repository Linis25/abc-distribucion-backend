namespace Application.DTOs.Product
{
    public record UpdateStockRequest(int palletId, int positionNumber, int NewQuantity);
}
