
using Orders.Models.DB;
using Orders.Models.DTO;
using Orders.Models.Filter;

namespace Orders.Service.Orders
{
    public interface IOrderService
    {
        Task<List<Order>> GetByFilterAsync(OrderFilter filter);
        Task<Order?> GetOnceById(int id);
        Task<Order?> CreateAsync(OrderDto order);
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);
    }
}
