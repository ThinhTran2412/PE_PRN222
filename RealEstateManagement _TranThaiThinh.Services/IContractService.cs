using RealEstateManagement__TranThaiThinh.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement__TranThaiThinh.Services
{
    public interface IContractService
    {
        Task<List<Contract>> GetAllAsync();

        Task<Contract> GetByIdAsync(int id);

        Task<List<Contract>> SearchAsync(string title, string? type, DateOnly date);

        Task<int> CreateAsync(Contract contract);

        Task<int> UpdateAsync(Contract contract);

        Task<bool> DeleteAsync(int id);

    }
}
