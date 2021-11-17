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
        public IEmployeeServices employeeServices;
        private readonly EmployeeContext _context;

        public EmployeeController(IEmployeeServices employeeServices, EmployeeContext context)
        {
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


   /*     [HttpGet("/getEmployee")]
        public ActionResult<List<EmployeeDTO>> GetEmploy()
        {
            var model = employeeServices.GetAllEmployee();
            return Ok(model);

           // await _context.Employees.Include(x => x.SocialMedia).ToListAsync();
        }*/

        [HttpGet("/getEmployee")]
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



    }
}
