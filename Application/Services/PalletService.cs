using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class PalletService(IPalletRepository _repository) : IPalletService
    {
        public async Task<List<Pallet>> GetAllAsync()
            => await _repository.GetAllAsync();
    }
}