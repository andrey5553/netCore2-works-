using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    //[Route("Users")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _EmployeesData;

        public EmployeesController(IEmployeesService EmployeesData) => _EmployeesData = EmployeesData;

        //[Route("All")]
        public IActionResult Index() => View(_EmployeesData.Get().ToView());

        //[Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }

        #region Edit

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int? id)
        {
            if (id is null) return View(new EmployeeViewModel());

            if (id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            if (Model is null)
                throw new ArgumentNullException(nameof(Model));

            if(Model.Age < 18 || Model.Age > 75)
                ModelState.AddModelError(nameof(Employee.Age), "Возраст должен быть всё же в пределах от 18 до 75");

            if(Model.FirstName == "123" && Model.SurName == "QWE")
                ModelState.AddModelError(string.Empty, "Странный выбор для имени и фамилии");

            if (!ModelState.IsValid)
                return View(Model);

            if (Model.Id == 0)
                _EmployeesData.Add(Model.FromView());
            else
                _EmployeesData.Edit(Model.FromView());

            _EmployeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            _EmployeesData.SaveChanges();

            return RedirectToAction(nameof(Index));
        } 

        #endregion
    }
}
