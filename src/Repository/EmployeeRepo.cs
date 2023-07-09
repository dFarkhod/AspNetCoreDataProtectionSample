using Virtualdars.DataProtectionSample.Controllers;
using Virtualdars.DataProtectionSample.Entities;

namespace Virtualdars.DataProtectionSample.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetById(Guid id);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        // private List<EmployeeModel> _employees = new List<EmployeeModel>();
        private AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            /* _employees.Add(new EmployeeModel { ActualId = Guid.NewGuid(), FullName = "Falonchi P", Email = "falonchi@virtualdars.com" });
             _employees.Add(new EmployeeModel { ActualId = Guid.NewGuid(), FullName = "Pistonchi F", Email = "pistonchi@virtualdars.com" });*/
            _dbContext = dbContext;
        }
        public List<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetById(Guid id)
        {
            return _dbContext.Employees.Where(emp => emp.Id.Equals(id)).FirstOrDefault();
        }
    }
}
