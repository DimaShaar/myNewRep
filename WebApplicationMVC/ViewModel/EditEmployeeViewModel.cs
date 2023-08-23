namespace WebApplicationMVC.ViewModel
{
    public class EditEmployeeViewModel:EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string? existingPhotoPath;
    }
}
