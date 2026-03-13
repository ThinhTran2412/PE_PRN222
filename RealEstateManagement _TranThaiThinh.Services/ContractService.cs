using RealEstateManagement__TranThaiThinh.Repositories;
using RealEstateManagement__TranThaiThinh.Repositories.Models;
using Contract = RealEstateManagement__TranThaiThinh.Repositories.Models.Contract;

namespace RealEstateManagement__TranThaiThinh.Services
{
    public class ContractService : IContractService
    {
        private readonly ContractRepository _repository;
        public ContractService() => _repository = new ContractRepository();


        public async Task<int> CreateAsync(Contract contract)
        {
            try
            {
                return await _repository.CreateAsync(contract);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item != null)
                {
                    return await _repository.RemoveAsync(item);

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<List<Contract>> GetAllAsync()
        {
            try
            {
                var items = await _repository.GetAllAsync();
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Contract>();
        }

        public async Task<Contract> GetByIdAsync(int id)
        {
            try
            {
                var item = await _repository.GetByIdAsync(id);
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }

        public async Task<List<Contract>> SearchAsync(string title, string? type , DateOnly date)
        {
            try
            {
                var items = await _repository.SearchAsync(title, type , date);
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return new List<Contract>();
        }

        public async Task<int> UpdateAsync(Contract contract)
        {
            try
            {
                return await _repository.UpdateAsync(contract);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return 0;
        }

    }
}
