using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.Products.InMemory
{
    public class InMemoryEmployeesData : IEmployeesService
    {
        private readonly List<Employee> _employees = TestData.Employees;

        public IEnumerable<Employee> Get() => _employees;

        public Employee GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee)) return employee.Id;

            employee.Id = _employees.Count == 0 ? 1 : _employees.Max(e => e.Id) + 1;
            _employees.Add(employee);
            return employee.Id;
        }

        public void Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (_employees.Contains(employee)) return;

            var db_employee = GetById(employee.Id);
            if (db_employee is null) return;

            db_employee.Name = employee.Name;
            db_employee.Surname = employee.Surname;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
        }

        public bool Delete(int id) => _employees.RemoveAll(e => e.Id == id) > 0;

        public void SaveChanges() { }
    }
}