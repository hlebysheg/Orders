using Orders.Models.DB;
using Orders.Models.DTO;

namespace Orders.Service.OrderItems
{
    public interface IOrderItemService
    {
        public List<OrderItem> AddList(List<OrderItemDto> items);

        public Task<OrderItem> AddOnceAsync(OrderItemDto item);

        public Task<OrderItem> GetOnceById(int id);

        public Task<OrderItem> EditAsync(OrderItem dto);

        public Task<bool> DeleteAsync(int id);

        public bool RemoveList(List<OrderItemDto> items);
    }
}
