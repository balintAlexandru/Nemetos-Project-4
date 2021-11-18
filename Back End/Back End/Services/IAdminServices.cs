using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{
    public interface IAdminServices
    {
        List<Admin> GetAdminList();
    }
}
