using Back_End.Data;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    public class AdminServices : IAdminServices
    {
        private AdministratorContext _dbContext;
        public AdminServices(AdministratorContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Admin> GetAdminList()
        {
            var users = _dbContext.Admins.ToList();
            return users;
        }
    }
}
