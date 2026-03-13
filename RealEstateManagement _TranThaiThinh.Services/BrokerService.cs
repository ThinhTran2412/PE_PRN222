using RealEstateManagement__TranThaiThinh.Repositories;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Services
{
    public class BrokerService
    {
        private readonly BrokerRepository _repository;

        public BrokerService()
        {
            _repository = new BrokerRepository();
        }

        public async Task<List<Broker>> GetAllAsync()
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
        }

    }
}
