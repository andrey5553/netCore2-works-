using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>API управления сотрудниками</summary>
    //[Route("api/[controller]")] // http://localhost:5001/api/EmployeesApi
    //[Route("api/employees")]      // http://localhost:5001/api/employees - наш выбор
    [Route(WebAPI.Employees)]      // http://localhost:5001/api/employees - наш выбор
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesService
    {
        private readonly IEmployeesService _employeesData;

        public EmployeesApiController(IEmployeesService employeesData) => _employeesData = employeesData;

        /// <summary>
        /// Получить всех доступных сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]        // GET http://localhost:5001/api/employees
        //[HttpGet("all")] // GET http://localhost:5001/api/employees/all
        public IEnumerable<Employee> Get() => _employeesData.Get();

        /// <summary>Найти сотрудника по идентификатору</summary>
        /// <param name="id">Идентификатор искомого сотрудника</param>
        /// <returns>Найденный сотрудник</returns>
        [HttpGet("{id}")]        // GET http://localhost:5001/api/employees/5
        public Employee GetById(int id) => _employeesData.GetById(id);

        /// <summary>Добавление нового сотрудника</summary>
        /// <param name="employee">Новый сотрудник</param>
        /// <returns>Идентификатор добавленного сотрудника</returns>
        [HttpPost]
        public int Add(Employee employee)
        {
            var id = _employeesData.Add(employee);
            SaveChanges();
            return id;
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        [HttpPut]
        public void Edit(Employee employee)
        {
            _employeesData.Edit(employee);
            SaveChanges();
        }

        /// <summary>Удаление</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")] // DELETE http://localhost:5001/api/employees/5
        //[HttpDelete("delete/{id}")]    // DELETE http://localhost:5001/api/employees/delete/5
        //[HttpDelete("delete({id})")]    // DELETE http://localhost:5001/api/employees/delete(5)
        public bool Delete(int id)
        {
            var result = _employeesData.Delete(id);
            SaveChanges();
            return result;
        }

        // Будет ошибка при автоматизированной генерации документации по WebAPI
        [NonAction]
        public void SaveChanges() => _employeesData.SaveChanges();
    }
}
