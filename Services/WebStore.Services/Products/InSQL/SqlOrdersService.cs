using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InSQL
{
    public class SqlOrderService : IOrdersService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreDB db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName)
        {
            var orders = await _db.Orders
               .Include(order => order.User)
               .Include(order => order.Items)
               .Where(order => order.User.UserName == UserName)
               .ToArrayAsync();
            return orders.Select(o => o.ToDTO());
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            var order = await _db.Orders
               .Include(o => o.User)
               .FirstOrDefaultAsync(o => o.Id == id);
            return order.ToDTO();
        }

        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user is null) throw new InvalidOperationException($"Пользователь {UserName} на найден");

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = OrderModel.Order.Name,
                Address = OrderModel.Order.Address,
                Phone = OrderModel.Order.Phone,
                User = user,
                Date = DateTime.Now
            };

            foreach (var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);
                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price, // здесь может быть применена скидка
                    Quantity = item.Quantity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);
            //await _db.OrderItems.AddRangeAsync(order.Items); // излишняя операция - элементы заказа и так попадут в БД
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order.ToDTO();
        }
    }
}