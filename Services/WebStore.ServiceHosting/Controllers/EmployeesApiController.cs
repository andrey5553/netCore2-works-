using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")] // http://localhost:5001/api/EmployeesApi
    //[Route("api/employees")]      // http://localhost:5001/api/employees - наш выбор
    [Route(WebAPI.Employees)]      // http://localhost:5001/api/employees - наш выбор
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesService
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesApiController(IEmployeesService employeesService) => _employeesService = employeesService;

        [HttpGet]        // GET http://localhost:5001/api/employees
        //[HttpGet("all")] // GET http://localhost:5001/api/employees/all
        public IEnumerable<Employee> Get() => _employeesService.Get();

        [HttpGet("{id}")]        // GET http://localhost:5001/api/employees/5
        public Employee GetById(int id) => _employeesService.GetById(id);

        [HttpPost]
        public int Add(Employee employee)
        {
            var id = _employeesService.Add(employee);
            SaveChanges();
            return id;
        }

        [HttpPut]
        public void Edit(Employee employee)
        {
            _employeesService.Edit(employee);
            SaveChanges();
        }

        [HttpDelete("{id}")] // DELETE http://localhost:5001/api/employees/5
        //[HttpDelete("delete/{id}")]    // DELETE http://localhost:5001/api/employees/delete/5
        //[HttpDelete("delete({id})")]    // DELETE http://localhost:5001/api/employees/delete(5)
        public bool Delete(int id)
        {
            var result = _employeesService.Delete(id);
            SaveChanges();
            return result;
        }

        // Будет ошибка при автоматизированной генерации документации по WebAPI
        [NonAction]
        public void SaveChanges() => _employeesService.SaveChanges();
    }
}