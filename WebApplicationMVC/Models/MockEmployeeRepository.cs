using Microsoft.EntityFrameworkCore;

namespace WebApplicationMVC.Models
{
    public class MockEmployeeRepository : IEmployeeReposirtory
    {
        private List<Employee> _employees;
        public MockEmployeeRepository()
        {
            _employees = new List<Employee>()
            {
                 new Employee() { Id=1,Name="Dima",Email="dima@yahoo.com",Department = Dept.HR},
                 new Employee() { Id=2,Name="Eman",Email="eman@yahoo.com",Department = Dept.Payroll},
                 new Employee() { Id=3,Name="Rawaa",Email="rawaa@yahoo.com",Department = Dept.None},
                 new Employee() { Id=4,Name="ana",Email="rawaa@yahoo.com",Department = Dept.None}

            };
        }

        public Employee Create(Employee employee)
        {
            employee.Id = _employees.Max(x => x.Id) + 1;
            _employees.Add(employee);   
            return employee;

        }

        public Employee Delete(int id)
        {
            Employee employee= _employees.FirstOrDefault(employee=>employee.Id==id);
            if (employee!=null)
            {
                _employees.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDepartment()
        {
            return _employees.GroupBy(d => d.Department)
                .Select(e => new DeptHeadCount
                {
                    Department = e.Key.Value,
                    Count = e.Count()
                }).ToList();
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
           return _employees;   
        }
        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        public Employee Update(Employee employeeChange)
        {
            Employee employee = _employees.FirstOrDefault(employee => employee.Id == employeeChange.Id);
            if (employee != null)
            {
                employee.Name = employeeChange.Name;    
                employee.Email = employeeChange.Email;  
                employee.Department = employeeChange.Department;
            }
            return employee;
        }
    }
}
