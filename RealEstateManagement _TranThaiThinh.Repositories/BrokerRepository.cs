using Microsoft.EntityFrameworkCore;
using RealEstateManagement__TranThaiThinh.Repositories.Basic;
using RealEstateManagement__TranThaiThinh.Repositories.Models;

namespace RealEstateManagement__TranThaiThinh.Repositories
{
    public class BrokerRepository : GenericRepository<Broker>
    {
        public BrokerRepository() { }

        public BrokerRepository(FA25RealEstateDBContext context) {
            _context = context;
        }   
    }
}
