﻿using Back_End.Data;
using Back_End.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminServices adminServices;
        public AdminController(IAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }
        [HttpGet("Login")]
        public IActionResult Get()
        {
            try
            {
                var users = adminServices.GetAdminList();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error has occured");
            }

        }
    }
}