using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;
namespace WebApplicationMVC.ViewComponent
{
    public class HeadCountViewComponent: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IEmployeeReposirtory _employeeReposirtory;
        public HeadCountViewComponent(IEmployeeReposirtory employeeReposirtory)
        {
            _employeeReposirtory = employeeReposirtory; 
        }
        
        public IViewComponentResult Invoke()
        {
            var result = _employeeReposirtory.EmployeeCountByDepartment();

            return View(result);
        
        } 
    }
}
