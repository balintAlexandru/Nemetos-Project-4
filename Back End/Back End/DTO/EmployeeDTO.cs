using Back_End.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Back_End.DTO
{
    public class EmployeeDTO
    {
        public int IdEmployee { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Intro { get; set; }
        
        public SocialMediaDTO SocialMedia { get; set; }


        public static Employee mappingDTOtoEntity(EmployeeDTO employeeDTO) {
            Employee employeeEntity = new Employee()
            {
                IdEmployee = employeeDTO.IdEmployee,
                Image = employeeDTO.Image,
                Name = employeeDTO.Name,
                Intro = employeeDTO.Intro,
                SocialMedia = null
            };

            return employeeEntity;
        }
    }
}
