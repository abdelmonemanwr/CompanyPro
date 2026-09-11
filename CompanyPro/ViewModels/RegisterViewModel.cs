using System.ComponentModel.DataAnnotations;

namespace CompanyPro.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        //[DataType()]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
