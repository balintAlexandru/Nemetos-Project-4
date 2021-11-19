using Back_End.Data;
using Back_End.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Back_End.Services
{
    public class AdminServices : IAdminServices
    {
        private EmployeeContext _dbContext;
        public AdminServices(EmployeeContext dbContext)
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
