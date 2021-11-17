using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using MVC_Company.Data;
using MVC_Company.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Company.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> AboutUs()
        {
            //List<Employee> model = await _context.Employees.Include(x => x.SocialMedia).ToListAsync();
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Portfolio()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [Route("ErrorPage")]
        public IActionResult ErrorPage()
        {
            return View("ErrorPage");
        }

    }
}
