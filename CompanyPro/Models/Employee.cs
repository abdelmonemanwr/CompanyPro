using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPro.Models
{
    public class Employee //: ApplicationUser
    {
        [Key]
        public int EId { get; set; }

        [MaxLength(50), MinLength(5)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50), MinLength(5)]
        public string Address { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [StringLength(100)]
        public string? ImageUrl { get; set; }


        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
