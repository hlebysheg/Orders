using Orders.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Models.DB
{
    public class OrderItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column("uit")]
        public string Unit { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        private OrderItem(int id, string name, decimal quantity, string unit)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }
        public OrderItem(int id, string name, decimal quantity, string unit, int orderId, Order order): this(id, name, quantity, unit)
        {
            OrderId = orderId;
            Order = order;
        }

        public OrderItem(OrderItemDto dto)
        {
            Name = dto.Name;
            Quantity = dto.Quantity;
            Unit = dto.Unit;
            OrderId = dto.OrderId;
        }
        public OrderItem(int id)
        {
            Id = id;
        }
        public OrderItem()
        {

        }
    }
}
