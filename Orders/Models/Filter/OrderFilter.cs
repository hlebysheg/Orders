using System.ComponentModel;

namespace Orders.Models.Filter
{
    public enum FilterColum
    {
        [Description("Фильтр по Id")]
        Id,
        [Description("Фильтр по поставщикам")]
        ProviderId,
        [Description("Фильтр по Дате")]
        Date,
        [Description("Фильтр по Номеру")]
        Number
    }
    public class OrderFilter
    {
        public int? ProviderId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndtDate { get; private set; }

        public FilterColum? Column { get; private set; }
        public OrderFilter(int? providerId, DateTime? startDate, DateTime? endtDate, FilterColum? col)
        {
            ProviderId = providerId == 0? null: providerId;
            StartDate = startDate ?? DateTime.UtcNow.AddMonths(-1);
            EndtDate = endtDate ?? DateTime.UtcNow;
            Column = col ?? FilterColum.Id;
        }

        public OrderFilter()
        {
            ProviderId =  null;
            StartDate = DateTime.UtcNow.AddMonths(-1);
            EndtDate = DateTime.UtcNow;
            Column = FilterColum.Id;
        }
    }
}
