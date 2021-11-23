using Back_End.Data;
using Back_End.DTO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{

    public interface IEmployeeServices {
        void AddEmployee(EmployeeDTO employee);
        void DeleteEmployee(int id);
        // List<EmployeeDTO> GetAllEmployee();
        void UpdateEmployee(EmployeeDTO employeeDTO);

    }

    public class EmployeeServices : IEmployeeServices
    {
        public EmployeeContext _db { get; set; }

        public EmployeeServices(EmployeeContext db) {
            this._db = db;
        }

        public void AddEmployee(EmployeeDTO employeeDTO)
        {
            var employeeEntity = EmployeeDTO.mappingDTOtoEntity(employeeDTO);
            var socialMediaEntity = SocialMediaDTO.mappingDTOtoEntity(employeeDTO.SocialMedia);
            try
            {
                employeeEntity.SocialMedia = socialMediaEntity;
                socialMediaEntity.Employees = employeeEntity;
                _db.SocialMedia.Add(socialMediaEntity);
                _db.Employees.Add(employeeEntity);
                _db.SaveChanges();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteEmployee(int id)
        {          
            try
            {
                var employee = _db.Employees.FirstOrDefault(x => x.IdEmployee == id);
                _db.Employees.Remove(employee);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdateEmployee(EmployeeDTO employeeDTO)
        {
            var employeeEntity = EmployeeDTO.mappingDTOtoEntity(employeeDTO);
         
            try
            {
                _db.Employees.Update(employeeEntity);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /*
        public List<EmployeeDTO> GetAllEmployee()
        {
            var employeeList = _db.Employees.ToList();
            var socialMediaList = _db.SocialMedia.ToList();
            var selectResult = employeeList.Select(x => new EmployeeDTO(){
              Name = x.Name,
              SocialMedia = x.SocialMedia
            });
            return selectResult;
        }*/


    }
}
