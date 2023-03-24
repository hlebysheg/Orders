namespace Orders.Models.Filter
{
    public class OrderFilter
    {
        public int? ProviderId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndtDate { get; private set; }

        public OrderFilter(int? providerId, DateTime? startDate, DateTime? endtDate)
        {
            ProviderId = providerId == 0? null: providerId;
            StartDate = startDate ?? DateTime.UtcNow.AddMonths(-1);
            EndtDate = endtDate ?? DateTime.UtcNow;
        }

        public OrderFilter()
        {
            ProviderId =  null;
            StartDate = DateTime.UtcNow.AddMonths(-1);
            EndtDate = DateTime.UtcNow;
        }
    }
}
