namespace Orders.Models.DTO
{
    public class OrderDto
    {
        public string Number { get; private set; }

        public DateTime Date { get; private set; }

        public int ProviderId { get; private set; }

        public List<OrderItemDto>? OrderItems { get; set; }

        public OrderDto(string number, int providerId, DateTime date)
        {
            Number = number;
            ProviderId = providerId;
            Date = date;
            OrderItems = null;
        }
    }
}
