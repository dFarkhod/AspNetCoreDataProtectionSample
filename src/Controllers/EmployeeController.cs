using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.VisualBasic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Web;
using Virtualdars.DataProtectionSample.Repository;
using Virtualdars.DataProtectionSample.Models;
using Mapster;
using Virtualdars.DataProtectionSample.Entities;

namespace Virtualdars.DataProtectionSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataProtector _protector;
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IDataProtectionProvider provider, IEmployeeRepository employeeRepo)
        {
            _protector = provider.CreateProtector("DataProtection.EmployeeController.v1");
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employeesInDb = _employeeRepo.GetAll();
            var employees = employeesInDb.Adapt<List<EmployeeModel>>();
            foreach (var emp in employees)
            {
                string id = _protector.Protect(emp.Id);
                emp.Id = HttpUtility.UrlEncode(id);
            }

            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetDetails(string employeeId)
        {
            var empGuid = Guid.Parse(_protector.Unprotect(employeeId));
            var employeeInDb = _employeeRepo.GetById(empGuid);
            if (employeeInDb == null)
            {
                return NotFound();
            }

            EmployeeModel employee = employeeInDb.Adapt<EmployeeModel>();
            string id = _protector.Protect(employee.Id);
            employee.Id = HttpUtility.UrlEncode(id);
            return Ok(employee);
        }


    }

   
}