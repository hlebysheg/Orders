using Orders.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Orders.Models.DB
{
    public class Order
    {
        [Required]
        public int Id { get; private set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        private Order(int id, string number, DateTime date)
        {
            Id = id;
            Number = number;
            Date = date;
        }

        public Order(int id, string number, DateTime date, int providerId, Provider? provider, List<OrderItem>? orderItems)
            : this(id, number, date)
        {
            Provider = provider;
            OrderItems = orderItems;
            ProviderId = providerId;
        }

        public Order(OrderDto dto)
         {
            Number = dto.Number;
            Date = dto.Date;
            ProviderId = dto.ProviderId;
        }
   }
}
