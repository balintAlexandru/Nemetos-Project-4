﻿using System.ComponentModel.DataAnnotations;

namespace MVC_Company.Entity
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
