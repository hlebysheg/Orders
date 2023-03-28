using Orders.Models.DB;
using Orders.Models.DTO;
using Orders.Models.Filter;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Orders.Service.Orders
{
    public class OrderService : IOrderService
    {
        OrderDbContext _ctx;
        private Dictionary<FilterColum, Expression<Func<Order, object>>> OrderExpression;
        public OrderService(OrderDbContext ctx)
        {
            _ctx = ctx;
            //todo add specification
            OrderExpression = new Dictionary<FilterColum, Expression<Func<Order, object>>>();
            OrderExpression.Add(FilterColum.Id, s => s.Id);
            OrderExpression.Add(FilterColum.ProviderId, s => s.ProviderId);
            OrderExpression.Add(FilterColum.Number, s => s.Number);
            OrderExpression.Add(FilterColum.Date, s => s.Date);

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
            var order = _ctx.Order.Find(id);
            if (order == null)
            {
                throw (new Exception("Not such element"));
            }

            _ctx.Order.Remove(order);
            await _ctx.SaveChangesAsync();
            return true;

        }

        public async Task<List<Order>> GetByFilterAsync(OrderFilter filter)
        {


            var isProviderFilter = filter.ProviderId != null;
            return await _ctx.Order.Include(el => el.Provider)
                                    .AsNoTracking()
                                    .Where(el =>
                                        (isProviderFilter ? el.ProviderId == filter.ProviderId : true)
                                        && el.Date >= filter.StartDate
                                        && el.Date <= filter.EndtDate
                                    )
                                    .OrderBy(OrderExpression[filter.Column ?? FilterColum.Id])
                                    .ToListAsync();
        }

        public async Task<Order?> GetOnceById(int id)
        {
            var Order = await _ctx.Order
                            .Include(el => el.OrderItems)
                            .FirstOrDefaultAsync(el => el.Id == id);
            //todo убрать багу (не грузится поле Unit)
            var OrderItems = await _ctx.OrderItem
                                        .AsNoTracking()
                                        .Where(el => el.OrderId == Order.Id)
                                        .Select(el => new OrderItem(el.Id)
                                        {
                                            OrderId = el.OrderId,
                                            Unit = el.Unit,
                                            Name = el.Name,
                                            Quantity = el.Quantity
                                        }).ToListAsync();
            Order.OrderItems = OrderItems;
            return Order;
        }

        public async Task<Order> UpdateAsync(int id, OrderDto dto)
        {
            var order = _ctx.Order.Find(id);
            if (order == null)
            {
                throw (new Exception("Not such element"));
            }
            order.Number = dto.Number;
            order.ProviderId = dto.ProviderId;
            order.Date = dto.Date;
            await _ctx.SaveChangesAsync();
            return order;
        }
    }
}
