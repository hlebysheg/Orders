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

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await GetOnceById(id);

            _ctx.OrderItem.Remove(item);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<OrderItem> EditAsync(OrderItem dto)
        {
            var item = await GetOnceById(dto.Id);

            item.Unit = dto.Unit;
            item.Quantity = dto.Quantity;
            item.Name = dto.Name;

            await _ctx.SaveChangesAsync();

            return item;
        }

        public async Task<OrderItem> GetOnceById(int id)
        {
            var item = await _ctx.OrderItem.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Not such item");
                return null;
            }

            return item;
        }

        public bool RemoveList(List<OrderItemDto> items)
        {
            throw new NotImplementedException();
        }
    }
}
