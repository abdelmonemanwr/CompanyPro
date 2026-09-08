using CompanyPro.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPro.ViewModels
{
    public class AddEmployeeViewModel
    {
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50), MinLength(5)]
        public string Address { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [StringLength(100)]
        public string? ImageUrl { get; set; }

        public int DepartmentId { get; set; }

        public List<DeptVM> depts { get; set; }

    }

    public class DeptVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
