using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using MVC_Company.Data;
using MVC_Company.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC_Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly string BaseURL = "https://localhost:44399/"; 
        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> AboutUs()
        {
            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(BaseURL + "Employees");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
            }
            return View(EmpInfo);
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
