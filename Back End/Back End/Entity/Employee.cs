using Back_End.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Entity
{
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Intro { get; set; }
        public SocialMedia SocialMedia { get; set; }
    }
}
