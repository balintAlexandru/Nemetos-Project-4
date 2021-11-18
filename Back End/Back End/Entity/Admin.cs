using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

<<<<<<< HEAD:Back End/Back End/Entity/Admin.cs
namespace MVC_Company.Entity
=======
namespace Back_End.Models
>>>>>>> 4091b242df8ac9a447c8e074654863e20601e95f:Back End/Back End/Models/Admin.cs
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
