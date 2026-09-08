using AutoMapper;
using CompanyPro.Models;
using CompanyPro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyPro.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IMapper mapper;
        private readonly ITIContext db = new();
        public DepartmentController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var depts = db.Departments.ToList();
            List<GetAllDepartmentsViewModel> deptsViewModel = new();
            foreach(var dept in depts)
            {
                deptsViewModel.Add(mapper.Map<GetAllDepartmentsViewModel>(dept));
            }
            return View("Index", deptsViewModel);

            //return View(depts);
        }

        //// Pure HTML
        //public IActionResult Add()
        //{
        //    return View("Add");
        //}

        //// Pure C#
        //public IActionResult Add()
        //{
        //    return View("Add_PureC#");
        //}

        // Tag Helper
        public IActionResult Add()
        {
            return View("Add_Tag_Helper");
        }

        [HttpPost]
        public IActionResult SaveNewDepartment(DepartmentViewModel deptVM)
        {
            //if(Request.Method != "Post")
            //{
            //    return NotFound("404-not found");
            //}

            // name="", loc="cairo"
            if (string.IsNullOrEmpty(deptVM.DepartmentName))
            {
                return View("Add_Tag_Helper", deptVM);
            }

            var newDepartment = new Department()
            {
                Name = deptVM.DepartmentName,
                Location = deptVM.DepartmentLocation,
            };
            db.Departments.Add(newDepartment);
            db.SaveChanges();
            return RedirectToAction("GetAllDepartments");

            //var depts = db.Departments.ToList();
            //List<GetAllDepartmentsViewModel> deptsViewModel = new();
            //foreach (var dept in depts)
            //{
            //    deptsViewModel.Add(mapper.Map<GetAllDepartmentsViewModel>(dept));
            //}
            //return View("Index", deptsViewModel);
        }
    }
}
