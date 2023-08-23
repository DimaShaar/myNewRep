using System.Collections;

namespace WebApplicationMVC.Models
{
    public interface IEmployeeReposirtory
    {
        public Employee GetEmployee(int id);
        public IEnumerable<Employee> GetAllEmployee();    
        public Employee Create(Employee employee);  
        public Employee Update(Employee employee);  
        public Employee Delete(int id); 
        public IEnumerable<DeptHeadCount> EmployeeCountByDepartment(); 
    }
}
