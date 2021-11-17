using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC_Company.Data;
using MVC_Company.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeContext _context;

        public HomeController(EmployeeContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> AboutUs()
        {
            List<Employee> model = await _context.Employees.Include(x => x.SocialMedia).ToListAsync();
            return View(model);
        }
   

    }
}
