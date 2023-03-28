using Orders.Models.DB;

namespace Orders.Service.Providers
{
    public interface IProviderService
    {
        Task<List<Provider>> GetProviderListAsync();
    }
}
