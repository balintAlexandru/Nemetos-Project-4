﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Company.Models
{
    public class Admin
    {
        
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }

}
