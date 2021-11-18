using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Company.Data;
using MVC_Company.DTO;
using MVC_Company.Entity;
using MVC_Company.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVC_Company.Controllers
{
    [Route("/api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public ISocialMediaServices socialMediaServices;
        public IEmployeeServices employeeServices;
        private readonly EmployeeContext _context;

        public EmployeeController(IEmployeeServices employeeServices, ISocialMediaServices socialMediaServices, EmployeeContext context)
        {
            this.socialMediaServices = socialMediaServices;
            this.employeeServices = employeeServices;
            this._context = context;
        }

        [HttpPost("/createEmploy")]
        public IActionResult CreateEmploy([FromBody] EmployeeDTO employeeDTO)
        {
            
            if (ModelState.IsValid)
            {

                employeeServices.AddEmployee(employeeDTO);

                return Ok(new { Message = "Employee was added" });
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("/addSocialMedia")]
        public IActionResult CreateSocial([FromBody]SocialMediaDTO socialMediaDTO,[FromRoute]int id) {
            if (ModelState.IsValid) 
            {
                socialMediaServices.addSocialMedia(socialMediaDTO, id);

                return Ok(new { Message = "Employee was added" });
            }
            return BadRequest();
        }


   /*     [HttpGet("/getEmployee")]
        public ActionResult<List<EmployeeDTO>> GetEmploy()
        {
            var model = employeeServices.GetAllEmployee();
            return Ok(model);

           // await _context.Employees.Include(x => x.SocialMedia).ToListAsync();
        }*/

        [HttpGet("/Employees")]
        public async Task<IActionResult> Get()
        {
            List<Employee> model = await _context.Employees.Include(x => x.SocialMedia).ToListAsync();
            return Ok(model);
        }

        [HttpGet("/getEmployee/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            Employee model = _context.Employees.SingleOrDefault(e => e.IdEmployee == id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpGet("/SocialMedia")]
        public async Task<IActionResult> GetSocialMedia()
        {
            List<SocialMedia> model =  _context.SocialMedia.ToList();
            return Ok(model);
        }



    }
}
