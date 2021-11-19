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
        public async Task<IActionResult> Validate(string username, string password, string returnUrl, CommonMethod methods)
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

            return View(EmpInfo);
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

            return Ok();
        }

        public async Task<IActionResult> SocialMedia()
        {
            //List<SocialMedia> model = await _context.SocialMedia.Include(x => x.Employees).ToListAsync();
            //  return View(await _context.Employees.Include(x => x.SocialMedia).ToListAsync());
            List<SocialMedia> socialMediasList = new List<SocialMedia>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(BaseURL + "SocialMedia");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    socialMediasList = JsonConvert.DeserializeObject<List<SocialMedia>>(EmpResponse);
                }
            }
            return View(socialMediasList);

        }
        public async Task<IActionResult> SocialMediaEdit(int? id)
        {
            /* if (id == null)
             {
                 return NotFound();
             }

             var media = await _context.SocialMedia.FindAsync(id);
             if (media == null)
             {
                 return NotFound();
             }*/
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SocialMediaEdit(int id, SocialMedia media)
        {
            /* if (id != media.IdSocialMedia)
             {
                 return NotFound();
             }
             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(media);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!SocialMediaExists(media.IdSocialMedia))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(SocialMedia));
             }*/
            return View();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            /* if (id == null)
             {
                 return NotFound();
             }

             var employee = await _context.Employees.FindAsync(id);
             if (employee == null)
             {
                 return NotFound();
             }*/
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, List<IFormFile> Image)
        {
            /* if (id != employee.IdEmployee)
             {
                 return NotFound();
             }
             foreach (var item in Image)
             {
                 if (item.Length > 0)
                 {
                     using (var stream = new MemoryStream())
                     {
                         await item.CopyToAsync(stream);
                         employee.Image = stream.ToArray();
                     }
                 }
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(employee);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!EmployeeExists(employee.IdEmployee))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Employee));
             }*/
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (employee == null)
            {
                return NotFound();
            }*/

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /* var employee = await _context.Employees.FindAsync(id);
             _context.Employees.Remove(employee);
             await _context.SaveChangesAsync();*/
            return RedirectToAction();
        }

        private bool EmployeeExists(int id)
        {
            //return _context.Employees.Any(e => e.IdEmployee == id);
            return false;
        }
        private bool SocialMediaExists(int id)
        {
            // return _context.SocialMedia.Any(e => e.IdSocialMedia == id);
            return false;
        }
    }
}






































































































































































































































































































































































































