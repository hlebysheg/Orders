using Orders.Models.DB;
using Orders.Models.DTO;
using Orders.Models.Filter;
using Microsoft.EntityFrameworkCore;

namespace Orders.Service.Orders
{
    public class OrderService : IOrderService
    {
        OrderDbContext _ctx;
        public OrderService(OrderDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Order?> CreateAsync(OrderDto orderDto)
        {
            var isSameOrder = _ctx.Order
                                .Where(el => 
                                    el.ProviderId == orderDto.ProviderId 
                                    && el.Number == orderDto.Number
                                 ).FirstOrDefault() != null;
			if (isSameOrder)
			{
                return null;
			}
            var order = new Order(orderDto);

            await _ctx.Order.AddAsync(order);
            await _ctx.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetByFilterAsync(OrderFilter filter)
        {
            var isProviderFilter = filter.ProviderId != null;
            return await _ctx.Order.Include(el => el.Provider)
                                    .Where(el => 
                                        (isProviderFilter ? el.ProviderId == filter.ProviderId : true) 
                                        &&el.Date >= filter.StartDate
                                        &&el.Date <= filter.EndtDate
                                    )
                                    .ToListAsync();
        }

        public async Task<Order?> GetOnceById(int id)
        {
            var Order = await _ctx.Order
                            .Include(el => el.OrderItems)
                            .FirstOrDefaultAsync(el => el.Id == id);
            //todo убрать багу (не грузится поле Unit)
            var OrderItems = await _ctx.OrderItem.Where(el => el.OrderId == Order.Id).Select(el => new OrderItem(el.Id)
            {
                OrderId = el.OrderId,
                Unit = el.Unit,
                Name = el.Name,
                Quantity = el.Quantity
            }).ToListAsync();
            Order.OrderItems = OrderItems;
            return Order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
