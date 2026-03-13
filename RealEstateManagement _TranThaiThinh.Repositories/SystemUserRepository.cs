using Microsoft.EntityFrameworkCore;
using RealEstateManagement__TranThaiThinh.Repositories.Basic;
using RealEstateManagement__TranThaiThinh.Repositories.Models;


namespace RealEstateManagement__TranThaiThinh.Repositories
{
    public class SystemUserRepository : GenericRepository<Contract>
    {
        public SystemUserRepository() { }

        public SystemUserRepository(FA25RealEstateDBContext context) {
            _context = context;
        }

        public async Task<SystemUser> GetAccount(String userName, String password)
        {

            return await _context.SystemUsers.FirstOrDefaultAsync(u => u.Username == userName && u.UserPassword == password);
        }

    }
} 
