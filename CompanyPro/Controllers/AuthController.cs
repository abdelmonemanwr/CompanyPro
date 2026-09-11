using CompanyPro.Models;
using CompanyPro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyPro.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITIContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(ITIContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // 2action-1view
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", userViewModel);
            }

            // save
            //UserManager u = new UserManager() // create

            ApplicationUser user = new ApplicationUser()
            {
                Email = userViewModel.Email,
                PhoneNumber = userViewModel.PhoneNumber,
                Address = userViewModel.Address,
                UserName = userViewModel.Name,
                PasswordHash = userViewModel.Password
                
            };


            var identityResult = await userManager.CreateAsync(user, userViewModel.Password);
            
            if (!identityResult.Succeeded)
            {
                foreach(var error in identityResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View("Register", userViewModel);
            }

            // create authentication cookie
            //SignInManager u = new SignInManager() // create

            //var claims = new List<Claim>()
            //{
            //    new Claim("org", "iti")
            //};
            await signInManager.SignInAsync(user, false);
            return RedirectToAction("GetAllDepartments", "Department");
        }


        // /auth/test/1   >> id=1
        // /auth/test     >> id=0
        //public IActionResult Test(int id)
        //{
        //    return Content($"id={id}");
        //}

        //public IActionResult Test()
        //{
        //    return Content("id=??");
        //}
    }
}
