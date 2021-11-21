using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back_End.Data;
using Back_End.DTO;
using Back_End.Entity;
using Back_End.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("/api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public ISocialMediaServices socialMediaServices;
        public IEmployeeServices employeeServices;
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            socialMediaServices = new SocialMediaServices(context);
            employeeServices = new EmployeeServices(context);
            _context = context;
        }

        [HttpPost("/createEmploy")]
        public IActionResult CreateEmployee([FromBody] EmployeeDTO employeeDTO)
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

        [HttpPost("/addSocialMedia")]
        public IActionResult CreateSocial([FromBody]SocialMediaDTO socialMediaDTO,[FromRoute]int id) {
            if (ModelState.IsValid) 
            {
                socialMediaServices.addSocialMedia(socialMediaDTO, id);

                return Ok(new { Message = "SocialMedia was added" });
            }
            return BadRequest();
        }

        [HttpGet("/SocialMedia")]
        public async Task<IActionResult> GetSocialMedia()
        {
            List<Employee> model = await _context.Employees.Include(x => x.SocialMedia).ToListAsync();
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            if(ModelState.IsValid)
            {
                employeeServices.DeleteEmployee(employeeDTO);
                return Ok(new { Message = "Employee was deleted" });
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
