using Microsoft.EntityFrameworkCore;
using RealEstateManagement__TranThaiThinh.Repositories.Basic;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Repositories
{
    public class ContractRepository : GenericRepository<Contract>
    {
        public ContractRepository() { }

        public ContractRepository(FA25RealEstateDBContext context)
        {
            _context = context;
        }

        public async Task<List<Contract>> GetAllAsync()
        {
            var items = await _context.Contracts
                .Include(p => p.Broker)
                .ToListAsync();

            return items ?? new List<Contract>();
        }

        public async Task<Contract> GetByIdAsync(int id)
        {
            var items = await _context.Contracts
                  .Include(p => p.Broker)
                  .FirstOrDefaultAsync(p => p.ContractId == id);
            return items ?? new Contract();
        }

        public async Task<List<Contract>> SearchAsync(string? title,string? type, DateOnly? date)
        {
            return await _context.Contracts
                .Include(c => c.Broker)
                .Where(c =>
                    (title != null && c.ContractTitle.Contains(title)) ||
                    (type != null && c.PropertyType.Contains(type)) ||
                    (date != null && c.SigningDate == date.Value)
                )
                .GroupBy(c => new
                {
                    c.ContractTitle,
                    c.PropertyType,
                    c.SigningDate
                })
                .Select(g => g.First())
                .ToListAsync();
        }


    }
}
