using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPalletRepository
    {
        Task<List<Pallet>> GetAllAsync();
    }
}