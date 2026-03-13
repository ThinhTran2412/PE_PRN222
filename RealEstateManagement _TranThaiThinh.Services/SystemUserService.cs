using RealEstateManagement__TranThaiThinh.Repositories;
using RealEstateManagement__TranThaiThinh.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagement__TranThaiThinh.Services
{
    public class SystemUserService
    {
        private readonly SystemUserRepository _repository;
        public SystemUserService() => _repository = new SystemUserRepository();

        public async Task<SystemUser> GetAccount(string username, string password)
        {
            try
            {
                return await _repository.GetAccount(username, password);
            }
            catch (Exception ex) { }
            return null;

        }

    }
}
