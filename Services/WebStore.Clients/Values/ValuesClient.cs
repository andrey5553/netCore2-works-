using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Interfaces.TestApi;

namespace WebStore.Clients.Values
{
    public class ValuesClient : BaseClient, IValueService
    {
        public ValuesClient(IConfiguration configuration) : base(configuration, "api/values") { }

        public IEnumerable<string> Get()
        {
            var response = Client.GetAsync(ServiceAddress).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<string>>().Result;

            return Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var response = Client.GetAsync($"{ServiceAddress}/{id}").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<string>().Result;

            return string.Empty;
        }

        public Uri Post(string value)
        {
            var response = Client.PostAsJsonAsync(ServiceAddress, value).Result;
            return response.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Update(int id, string value)
        {
            var response = Client.PutAsJsonAsync($"{ServiceAddress}/{id}", value).Result;
            return response.EnsureSuccessStatusCode().StatusCode;
        }

        public HttpStatusCode Delete(int id)
        {
            var response = Client.DeleteAsync($"{ServiceAddress}/{id}").Result;
            return response.StatusCode;
        }
    }
}
