using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Maximum length is 50 Character")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string? photopath { get; set; }
    }
}
