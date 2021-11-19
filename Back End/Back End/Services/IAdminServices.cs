using Back_End.Entity;
using System.Collections.Generic;

namespace Back_End.Services
{
    public interface IAdminServices
    {
        List<Admin> GetAdminList();
    }
}
