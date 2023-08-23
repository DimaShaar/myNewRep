namespace WebApplicationMVC.Models
{
    public class SqlEmployeeRepository : IEmployeeReposirtory
    {
        private readonly AppDbContext _context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public Employee Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDepartment()
        {
            return _context.Employees.GroupBy(d=>d.Department)
                .Select( e => new DeptHeadCount
                {
                    Department = e.Key.Value,
                    Count = e.Count()
                }).ToList();                    
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee Update(Employee employeeChange)
        {
            var employee = _context.Employees.Attach(employeeChange);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employeeChange;
        }
    }
}
