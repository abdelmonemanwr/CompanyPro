using System.ComponentModel.DataAnnotations;

namespace CompanyPro.ViewModels
{
    public class DepartmentViewModel
    {
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        public string DepartmentLocation { get; set; }

        // just for test
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Salary { get; set; }
        public bool IsValid { get; set; }

    }
}
