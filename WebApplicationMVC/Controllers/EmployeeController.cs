using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVC.Models;
using WebApplicationMVC.ViewModel;

namespace WebApplicationMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeReposirtory _employeeReposirtory;
        private IWebHostEnvironment _webHostEnvironment;  

        public EmployeeController(IEmployeeReposirtory employeeReposirtory, IWebHostEnvironment webHostEnvironment)
        {
            _employeeReposirtory = employeeReposirtory;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var model = _employeeReposirtory.GetAllEmployee();
            ViewBag.Title = "Index";
            return View(model);
        }
        [AllowAnonymous]
        public ViewResult Details(int id)
        {
            //throw new Exception("Error in Details View");

            Employee employee = _employeeReposirtory.GetEmployee(id);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }

            EmployeeDetailsViewModels employeeDetailsViewModels = new EmployeeDetailsViewModels()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            return View(employeeDetailsViewModels);
        }
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }
       
        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeReposirtory.GetEmployee(id);
            EditEmployeeViewModel editEmployeeViewModelcs = new EditEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                existingPhotoPath = employee.photopath
            };
            return View(editEmployeeViewModelcs);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UniqueFileName = ProcessUploadedFile(model);
                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    photopath = UniqueFileName
                };
               var employeeCreated =_employeeReposirtory.Create(newEmployee);
                return RedirectToAction("Details", new { id = employeeCreated.Id });
            }

            return View();

        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(EditEmployeeViewModel model)
        {                   
            if (ModelState.IsValid)
            {
                Employee employee = _employeeReposirtory.GetEmployee(model.Id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.photo != null)
                {
                    if(model.existingPhotoPath != null)
                    {
                       string filePath = Path.Combine(_webHostEnvironment.WebRootPath ,"images",model.existingPhotoPath);
                       System.IO.File.Delete(filePath);
                    }
                    employee.photopath = ProcessUploadedFile(model);

                }                
                _employeeReposirtory.Update(employee);
                return RedirectToAction("index","home");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string UniqueFileName = "";
            if (model.photo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filePath = Path.Combine(uploadFolder, UniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.photo.CopyTo(fileStream);
                }
            }

            return UniqueFileName;
        }
               
    }
}
