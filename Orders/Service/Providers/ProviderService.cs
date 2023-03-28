using Orders.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace Orders.Service.Providers
{
    public class ProviderService : IProviderService
    {
        OrderDbContext _ctx;
        public ProviderService(OrderDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Provider>> GetProviderListAsync()
        {
            return await _ctx.Provider.AsNoTracking().ToListAsync();
        }
    }
}
