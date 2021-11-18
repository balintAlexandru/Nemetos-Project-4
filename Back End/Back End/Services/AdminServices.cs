using Back_End.Data;
using MVC_Company.Entity;
using System.Collections.Generic;
using System.Linq;

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
