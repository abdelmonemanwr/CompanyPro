using Microsoft.AspNetCore.Identity;

namespace CompanyPro.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? Address { get; set; }
    }
}
