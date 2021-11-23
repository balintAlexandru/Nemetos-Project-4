using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Company.Models;
using MVC_Company.RequestServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC_Company.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IRequestServices requestServices { get; set; }
        private readonly string BaseURL = "https://localhost:44399/";
        public AdminController(IRequestServices requestServices)
        {
            this.requestServices = requestServices;
        }
        
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(Admin admin, string returnUrl)
        {

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> ValidateAdmin(string username, string password, string returnUrl, CommonMethod methods)
        {
            List<Admin> AdminInfo = new List<Admin>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Admin");
                if (Res.IsSuccessStatusCode)
                {
                    var AdminResponse = Res.Content.ReadAsStringAsync().Result;
                    AdminInfo = JsonConvert.DeserializeObject<List<Admin>>(AdminResponse);
                }

            }
            foreach (var item in AdminInfo)
            {
                if (username == item.UserName && password == methods.ConvertToDecrypt(item.Password))
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", username));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return Redirect(returnUrl);
                }
            }
            return RedirectToAction();
        }

        [HttpGet]
        public async Task<IActionResult> Employee()
        {
            List<Employee> EmpInfo = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(BaseURL + "employees");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
            }

            return View("employee",EmpInfo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee, IFormFile Image)
        {
            requestServices.CreateEmployee(Image, employee);
            
            return RedirectToAction("Employee");
        }

        public async Task<IActionResult> SocialMedia()
        {
            List<Employee> employeeList = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(BaseURL + "SocialMedia");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    employeeList = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                }
            }
            return View(employeeList);

        }
        public async Task<IActionResult> SocialMediaEdit(int? id)
        { 
            //TODO
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SocialMediaEdit(int id, SocialMedia media)
        {
            requestServices.UpdateSocialMedia(id, media);
            return RedirectToAction("SocialMedia");
        }
        public async Task<IActionResult> Edit(int? id)
        {          
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile Image)
        {

            requestServices.UpdateEmployee(Image, employee,id);
            return RedirectToAction("Employee");
            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Employee empinfo = new Employee();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(BaseURL + "getEmployee/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    empinfo = JsonConvert.DeserializeObject<Employee>(EmpResponse);
                }
            }
            return View(empinfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /* var employee = await _context.Employees.FindAsync(id);
             _context.Employees.Remove(employee);
             await _context.SaveChangesAsync();*/
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.DeleteAsync(BaseURL + "Delete/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                }
            }
            return RedirectToAction();
        }

    }
}






































































































































































































































































































































































































