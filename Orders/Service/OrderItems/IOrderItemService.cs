using Orders.Models.DB;
using Orders.Models.DTO;

namespace Orders.Service.OrderItems
{
    public interface IOrderItemService
    {
        public List<OrderItem> AddList(List<OrderItemDto> items);

        public Task<OrderItem> AddOnceAsync(OrderItemDto item);

        public bool RemoveList(List<OrderItemDto> items);
    }
}
