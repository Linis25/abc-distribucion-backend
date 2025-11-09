using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PalletRepository(AppDbContext context) : IPalletRepository
    {
        public async Task<List<Pallet>> GetAllAsync()
        {
            return await context.Pallet.ToListAsync();
        }
    }
}
