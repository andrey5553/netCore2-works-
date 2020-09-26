using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebStore.Infrastructure
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            //Действия над context до следующего элемента в конвейере
            await _next(context); // Вызов следующего промежуточного ПО в конвейере
            // Действия над context после следующего элемента в конвейере
        }
    }
}
