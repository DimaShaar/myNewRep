using System.ComponentModel.DataAnnotations;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.ViewModel
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Maximum length is 50 Character")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public IFormFile photo { get; set; }
    }
}
