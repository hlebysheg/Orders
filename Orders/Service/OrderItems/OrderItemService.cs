using Orders.Models.DB;
using Orders.Models.DTO;

namespace Orders.Service.OrderItems
{
    public class OrderItemService : IOrderItemService
    {
        OrderDbContext _ctx;
        public OrderItemService(OrderDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<OrderItem> AddList(List<OrderItemDto> items)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderItem> AddOnceAsync(OrderItemDto item)
        {
            var orderItem = new OrderItem(item);
            await _ctx.OrderItem.AddAsync(orderItem);
            await _ctx.SaveChangesAsync();
            return orderItem;
        }

        public bool RemoveList(List<OrderItemDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
