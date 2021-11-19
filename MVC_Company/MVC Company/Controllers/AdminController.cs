using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Company.Data;
using MVC_Company.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC_Company.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly EmployeeContext _context;

        public AdminController(EmployeeContext context)
        {
            _context = context;
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
            foreach (var item in _context.Admins)
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
            return RedirectToAction(nameof(Employee));
        }
        public async Task<IActionResult> Employee()
        { 
            return View(await _context.Employees.Include(x => x.SocialMedia).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, List<IFormFile> Image)
        {
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
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Employee));
        }
        public async Task<IActionResult> SocialMedia()
        {
            List<SocialMedia> model = await _context.SocialMedia.Include(x => x.Employees).ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> SocialMediaEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.SocialMedia.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }
            return View(media);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SocialMediaEdit(int id, SocialMedia media)
        {
            if (id != media.IdSocialMedia)
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
            }
            return View(media);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, List<IFormFile> Image)
        {
            if (id != employee.IdEmployee)
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
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Employee));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.IdEmployee == id);
        }
        private bool SocialMediaExists(int id)
        {
            return _context.SocialMedia.Any(e => e.IdSocialMedia == id);
        }
    }
}






































































































































































































































































































































































































