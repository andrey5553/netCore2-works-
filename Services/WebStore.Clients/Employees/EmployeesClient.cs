using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesService
    {
        public EmployeesClient(IConfiguration _configuration) : base(_configuration, WebAPI.Employees) { }

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(_serviceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_serviceAddress}/{id}");

        public int Add(Employee employee) => Post(_serviceAddress, employee).Content.ReadAsAsync<int>().Result;

        public void Edit(Employee employee) => Put(_serviceAddress, employee);

        public bool Delete(int id) => Delete($"{_serviceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }
    }
}