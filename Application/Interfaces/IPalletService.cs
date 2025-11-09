using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPalletService
    {
        Task<List<Pallet>> GetAllAsync();
    }
}
