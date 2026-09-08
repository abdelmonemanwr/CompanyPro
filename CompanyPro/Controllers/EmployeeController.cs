using AutoMapper;
using CompanyPro.Models;
using CompanyPro.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyPro.Controllers
{
    public class EmployeeController : Controller
    {

        private ITIContext dbContext = new ITIContext();  // Consist_of

        private readonly IMapper _mapper;
        public EmployeeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //using Auto Mapper
        public IActionResult GetAllEmployees()
        {
            // extra data
            string msg = "this is lec2";
            int temp = 38;
            List<string> Branches = ["Smart", "Alex", "MNF", "Benha"];
            string color = "blue";

            //var employeesModel = dbContext.Employees.Include(emp=>emp.Department).ToList(); // eager loading
            var employeesModel = dbContext.Employees.ToList(); // lazy loading

            List<GetAllEmployeesViewModel> employeesViewModel = new();
            foreach (var empModel in employeesModel)
            {
                GetAllEmployeesViewModel empViewModel = _mapper.Map<GetAllEmployeesViewModel>(empModel);
             
                empViewModel.Branches = Branches;
                empViewModel.Message = msg;
                empViewModel.Temp = temp;
                empViewModel.Color = (empModel.EId % 2 == 0) ? color : "green";
                employeesViewModel.Add(empViewModel);
            }

            return View("ShowAllEmployeesViewModel", employeesViewModel);
        }

        //public IActionResult GetAllEmployees()
        //{
        //    // extra data
        //    string msg = "this is lec2";
        //    int temp = 38;
        //    List<string> Branches = ["Smart", "Alex", "MNF", "Benha"];
        //    string color = "blue";

        //    //var employeesModel = dbContext.Employees.Include(emp=>emp.Department).ToList();
        //    var employeesModel = dbContext.Employees.ToList();

        //    List<GetAllEmployeesViewModel> employeesViewModel = new();
        //    foreach (var empModel in employeesModel)
        //    {
        //        employeesViewModel.Add(new GetAllEmployeesViewModel()
        //        {
        //            Eid = empModel.EId,
        //            Branches = Branches,
        //            DeptName = empModel.Department.Name,
        //            Message = msg,
        //            EmpName = empModel.Name,
        //            Salary = empModel.Salary,
        //            Temp = temp,
        //            Address = empModel.Address,
        //            Color = (empModel.EId%2==0)?color:"green",
        //        });
        //    }

        //    return View("ShowAllEmployeesViewModel", employeesViewModel);
        //}

        //public IActionResult GetAllEmployees()
        //{
        //    // extra data
        //    string msg = "this is lec2";
        //    int temp = 38;
        //    List<string> Branches = ["Smart", "Alex", "MNF", "Benha"];
        //    string color = "blue";

        //    // 1. ViewData
        //    // Dictionary<string, object> ViewData;
        //    ViewData["Color"] = color;
        //    ViewData["Temp"] = temp;
        //    ViewData["Msg"] = "This is MVC course";
        //    ViewBag.Color = "green";

        //    // 2. ViewBag
        //    ViewBag.Msg = msg;
        //    ViewBag.branches = Branches;

        //    //var employeesModel = dbContext.Employees.Include(emp=>emp.Department).ToList();
        //    var employeesModel = dbContext.Employees.ToList();
        //    return View("ShowAllEmployees", employeesModel);
        //}

        public IActionResult GetEmployeeById(int id, int age, string name)
        {
            Console.WriteLine($"id={id}, age={age}, name={name}\n");
            var employeeModel = dbContext.Employees.FirstOrDefault(e => e.EId == id);
            return View("GetEmployeeById", employeeModel);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var deptsList = dbContext.Departments.Select(d => new DeptVM { Id = d.Did, Name = d.Name }).ToList();
            ViewBag.Depts = deptsList;
            return View("AddNewEmployee");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNew(AddEmployeeViewModel empVM)
        {
            if (empVM.DepartmentId < 0 || string.IsNullOrEmpty(empVM.Name))
            {
                var deptsList = dbContext.Departments.Select(d => new DeptVM { Id = d.Did, Name = d.Name }).ToList();
                ViewBag.Depts = deptsList;
                return View("AddNewEmployee", empVM);
            }

            var emp = new Employee()
            {
                Name = empVM.Name,
                Salary = empVM.Salary,
                DepartmentId = empVM.DepartmentId,
                Address = empVM.Address,
                ImageUrl = empVM.ImageUrl,
            };

            dbContext.Employees.Add(emp);
            dbContext.SaveChanges();
            return RedirectToAction("GetAllEmployees");
        }





        //// employee/test
        //[HttpGet]
        //public IActionResult test()
        //{
        //    return Content("");
        //}

        // employee/test
        // employee/test?x=11
        //[HttpGet]
        //public IActionResult test(int x)
        //{
        //    return Content("");
        //}


    }
}
