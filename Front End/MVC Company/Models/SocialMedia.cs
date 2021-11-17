using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Company.Models
{
    public class SocialMedia
    {
        [Key]
        public int IdSocialMedia { get; set; }
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public int EmployeesRef { get; set; }
        public Employee Employees { get; set; }
    }
}
