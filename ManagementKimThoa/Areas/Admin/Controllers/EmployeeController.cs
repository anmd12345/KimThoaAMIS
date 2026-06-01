using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.Employee;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManagementKimThoa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;

        public EmployeeController(IEmployeeService employeeService, IRoleService roleService)
        {
            _employeeService = employeeService;
            _roleService = roleService;
        }


        [Route(RouteConstant.Employees)]
        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var employees =
            await _employeeService.GetEmployeesAsync();

            return View(employees);
        }


        [Route(RouteConstant.EmployeeDetail + "/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> EmployeeDetail(int id)
        {

            var employee = await _employeeService.GetByIdAsync(id);

            return View(employee);
        }

        [Route(RouteConstant.CreateEmployee)]
        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            var roles = await _roleService.GetAllAsync();


            ViewBag.roles = roles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route(RouteConstant.CreateEmployee)]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _employeeService.Create(dto);

            TempData["Success"] = "Thêm nhân sự thành công";

            return Redirect(RouteConstant.CreateEmployee);
        }
    }
}

